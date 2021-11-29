using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.Abilities;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleBoard : MonoBehaviour, IInterfear
    {
        private static int riddleBoardId;
        public static void ResetIDs() {
            riddleBoardId = 0;
        }

        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        [SerializeField] ScriptableRiddleCollection riddleCollection;
        [SerializeField] RiddleSolutionPad mySolutionPad;
        private TextMeshProUGUI myText;
        private Riddle myRiddle;

        private RiddlerInputPanel inputPanel;
        private bool underSpell;
        private bool timerOn;
        private float timer;
        private float spellDuration = 12;

        private int myId;
        private DataToSend dataToSend;

        private void Start() {
            myId = riddleBoardId;
            riddleBoardId++;
            dataToSend = new DataToSend(myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.riddleBoardEvent.AddListener(RecieveData);

            inputPanel = FindObjectOfType<RiddlerInputPanel>();
            SetUpBoard();
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId)
                StartInterfere(extracted.input);
        }

        private void SendData(string inputValue) {
            dataToSend.input = inputValue;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(myText != null)
                myText.gameObject.SetActive(false);
            if(!DevBoy.yes)
                NetUnityEvents.instance.riddleBoardEvent.RemoveListener(RecieveData);
        }

        private void SetUpBoard() {
            myRiddle = riddleCollection.GetRandomRiddle();
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myRiddle.riddle;
            mySolutionPad.SetSolution(myRiddle.answer, inputPanel);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= spellDuration) {
                    timerOn = false;
                    underSpell = false;
                    myText.text = myRiddle.riddle;
                }
            }
        }

        public void ChangeText(string value) {
            StartInterfere(value);
            
            if(!DevBoy.yes)
                SendData(value);
        }

        private void StartInterfere(string value) {
            myText.text = value;
            timer = 0;
            timerOn = true;
            underSpell = true;
        }

        public bool CanInterfear() {
            return !underSpell;
        }

        public void Interfear() {
            inputPanel.OpenPanel(this);
        }

        private class ExtractionClass {
            public int myId;
            public string input;
        }

        private class DataToSend {
            public int myId;
            public string input;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "riddleBoard";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}