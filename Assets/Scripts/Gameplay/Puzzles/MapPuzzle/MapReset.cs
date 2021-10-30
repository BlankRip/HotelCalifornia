using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapReset : MonoBehaviour, IInteractable
    {
        [SerializeField] MapSolution solution;
        private DataToSend startData;
        private void Start()
        {
            startData = new DataToSend("resetMapConnections");
            if (!DevBoy.yes)
                NetUnityEvents.instance.mixerEvents.AddListener(RecieveData);
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.mixerEvents.RemoveListener(RecieveData);
        }

        public void Interact()
        {
            solution.ResetConnections();
        }

        public void HideInteractInstruction() { }

        public void ShowInteractInstruction() { }

        private void SendStartMixerData()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(startData));
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.eventName == "resetMapConnections")
                solution.ResetConnections();
        }

        private class ExtractionClass
        {
            public string eventName;
        }

        private class DataToSend
        {
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(string eventName)
            {
                this.eventName = eventName;
                distributionOption = DistributionOption.serveOthers;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}