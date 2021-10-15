using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PuzzleSolvedObj : MonoBehaviour, IInteractable
    {
        #region DONT LOOK
        private static int solutionID;
        public static void ResetIDs()
        {
            solutionID = 0;
        }
        #endregion
        DataToSend dataToSend;
        [SerializeField] ScriptablePuzzleStatusTracker exitDoor;
        private Collider myCollider;
        private int myId = 0;

        private void Start() {
            myId = solutionID;
            solutionID++;
            dataToSend = new DataToSend(myId);

            myCollider = GetComponent<Collider>();
            if(!DevBoy.yes)
                NetUnityEvents.instance.puzzleSolvedEvent.AddListener(ReceiveData);
        }
        
        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.puzzleSolvedEvent.RemoveListener(ReceiveData);
        }

        public void Interact() {
            exitDoor.tracker.OnePuzzleSolved();
            myCollider.enabled = false;
            if(!DevBoy.yes)
                SendData();
        }

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {
            Debug.Log("InteractNow");
        }

        public void SendData()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        public void ReceiveData(string datastring)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(datastring);
            if(extracted.myId == myId) 
            {
                exitDoor.tracker.OnePuzzleSolved();
                myCollider.enabled = false;
            }
        }

        private class DataToSend {
            public int myId;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "puzzleSolveButton";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }

        private class ExtractionClass
        {
            public int myId;
        }
    }
}