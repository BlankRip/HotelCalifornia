using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseButton : MonoBehaviour, IInteractable
    {
        public static int morseButtonId;
        public static void ResetIDs() {
            morseButtonId = 0;
        }

        public char myValue;
        [SerializeField] string textPoolTag = "MorseText";
        [SerializeField] Transform textPos;

        private IMorseDevice device;
        private MorseAlphPanel panel;
        private TextMeshProUGUI text;
        bool inverse;

        private int myId;
        private DataToSend dataToSend;

        private void Start()
        {
            myValue = '0';
            device = GetComponentInParent<IMorseDevice>();
            panel = FindObjectOfType<MorseAlphPanel>();
            text = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.transform.position,
                textPos.transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = myValue.ToString();

            myId = morseButtonId;
            morseButtonId++;
            dataToSend = new DataToSend(myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.morseButtonEvent.AddListener(RecieveData);
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId)
                SetTextOffline(extracted.charValue);
        }

        private void SendData(char charValue) {
            dataToSend.charValue = charValue;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.morseButtonEvent.RemoveListener(RecieveData);
        }

        public void SetText(char value) {
            SetTextOffline(value);
            if(!DevBoy.yes)
                SendData(value);
        }
        private void SetTextOffline(char value) {
            myValue = value;
            text.text = myValue.ToString();
            device.CheckSolution();
        }

        public void Interact()
        {
            panel.OpenPanel(this);
        }

        public void ShowInteractInstruction() {}
        public void HideInteractInstruction() {}


        private class ExtractionClass {
            public int myId;
            public char charValue;
        }

        private class DataToSend {
            public int myId;
            public char charValue;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "MorseButton";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}