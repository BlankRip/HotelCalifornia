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
        public ClipName myClip;
        [SerializeField] TextMeshProUGUI text;
        bool inverse;
        private int id;
        private DataToSend dataToSend;

        private void Start()
        {
            myClip = ClipName.MorseA;
            text.text = myClip.ToString();
        }

        public void Interact()
        {
            CycleValue();
            SendData();
        }

        private void CycleValue()
        {
            if (myClip >= ClipName.MorseC)
                inverse = true;
            else if (myClip <= ClipName.MorseA)
                inverse = false;
            if (!inverse)
                myClip++;
            else
                myClip--;
            text.text = myClip.ToString();
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.myId == id)
            {
                CycleValue();
            }
        }

        private void SendData()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        public void ShowInteractInstruction() {}
        public void HideInteractInstruction() {}

        private class ExtractionClass
        {
            public int myId;
        }
        
        private class DataToSend
        {
            public int myId;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id)
            {
                eventName = "MorseButtonClick";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }

            public void HideInteractInstruction() { }

            public void ShowInteractInstruction() { }
        }
    }
}