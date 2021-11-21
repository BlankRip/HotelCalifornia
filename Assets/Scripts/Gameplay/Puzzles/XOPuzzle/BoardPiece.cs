using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class BoardPiece : MonoBehaviour, IInteractable
    {
        private static int pieceId;
        public static void ResetIDs() {
            pieceId = 0;
        }

        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] string textPoolTag;
        private List<string> values;
        private int index;
        private TextMeshProUGUI myText;
        private IPuzzleBoard board;
        private bool delusional;
        private int id;
        private DataToSend dataToSend;
        
        private void Start() {
            BasicSetUp();
            board = GetComponentInParent<IPuzzleBoard>();

            delusional = false;
            eventCollection.twistVision.AddListener(TwistVision);
            eventCollection.fixVision.AddListener(BackToNormalVision);

            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if(!DevBoy.yes)
                NetUnityEvents.instance.xoPieceEvent.AddListener(RecieveData);
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == id) {
                CylceValue();
            }
        }

        private void SendData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(myText != null)
                myText.gameObject.SetActive(false);
            if(delusional)
                FlipXO();
            eventCollection.twistVision.RemoveListener(TwistVision);
            eventCollection.fixVision.RemoveListener(BackToNormalVision);

            if(!DevBoy.yes)
                NetUnityEvents.instance.xoPieceEvent.RemoveListener(RecieveData);
        }

        private void TwistVision() {
            delusional = true;
            FlipXO();
        }

        private void BackToNormalVision() {
            delusional = false;
            FlipXO();
        }

        private void BasicSetUp() {
            if(myText == null) {
                values = new List<string>();
                values.Add("");
                values.Add("X");
                values.Add("O");
                myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, transform.position, transform.rotation).GetComponent<TextMeshProUGUI>();
                myText.text = values[index];
            }
        }

        public void Interact() {
            CylceValue();
            if(!DevBoy.yes)
            SendData();
        }

        private void CylceValue() {
            if(index == 2)
                index = 0;
            else
                index++;
            myText.text = values[index];
            if(delusional)
                FlipXO();
            board.CheckSolution();
        }

        private void FlipXO() {
            if(myText.text == "X")
                myText.text = "O";
            else if(myText.text == "O")
                myText.text = "X";
        }

        public string GetValue() {
            return values[index];
        }

        public void SetRandom() {
            BasicSetUp();
            index = Random.Range(1, 3);
            myText.text = values[index];
        }

        public void ShowInteractInstruction() {
            InstructionText.instance.ShowInstruction("Press \'LMB\' To Swap Value");
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        private class ExtractionClass {
            public int myId;
        }

        private class DataToSend {
            public int myId;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "XOBoardPiece";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}