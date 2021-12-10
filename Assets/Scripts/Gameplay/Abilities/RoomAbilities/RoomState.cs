using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Abilities {
    public class RoomState : MonoBehaviour, IRoomState, ICancelableTrap
    {
        private static int currentId;
        [SerializeField] ScriptableTrapTracker trapTracker;
        [SerializeField] bool startRoom;
        private RoomEffectState roomState;
        private List<IAbilityResetter> resetters;
        private float resetIn;
        private bool onTimer;
        private int id;
        private RoomStateData dataToSend;

        private void Awake() {
            if(startRoom)
                currentId = 0;
            id = currentId;
            currentId++;
            dataToSend = new RoomStateData((int)roomState, id, 0);
            if(!DevBoy.yes)
                NetUnityEvents.instance.roomTiggerOnMsgRecieve.AddListener(ReadData);
            resetters = new List<IAbilityResetter>();
        }

        private void Start() {
            if(startRoom)
                roomState = RoomEffectState.NoAbility;
            else
                trapTracker.tracker.AddToCancelable(this);
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.roomTiggerOnMsgRecieve.RemoveListener(ReadData);
            if(!startRoom)
                trapTracker.tracker.RemoveFromCancelable(this);
        }

        private void SendData() {
            dataToSend.roomState = (int)roomState;
            dataToSend.timerTime = resetIn;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void ReadData(string recieved) {
            RoomIdExtraction check = JsonUtility.FromJson<RoomIdExtraction>(recieved);
            if(id == check.myId)
                SetRoomState((RoomEffectState)check.roomState, check.timerTime, false);
        }

        private void Update() {
            if(onTimer) {
                resetIn -= Time.deltaTime;
                if(resetIn <= 0)
                    Cancel();
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Ghost"))
                AddToResetters(other.gameObject);
        }

        private void AddToResetters(GameObject go) {
            IAbilityResetter[] currentResetters = go.GetComponents<IAbilityResetter>();
            if(currentResetters != null) {
                foreach (IAbilityResetter reset in currentResetters)
                    resetters.Add(reset);
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Ghost"))
                RemoveFromResetters(other.gameObject);
        }

        private void RemoveFromResetters(GameObject go) {
            IAbilityResetter[] currentResetters = go.GetComponents<IAbilityResetter>();
            if(currentResetters != null) {
                foreach (IAbilityResetter reset in currentResetters) {
                    if(resetters.Contains(reset))
                        resetters.Remove(reset);
                }
            }
        }

        public RoomEffectState GetRoomState() {
            return roomState;
        }

        public bool CanChangeState() {
            if(resetters.Count == 0 && roomState == RoomEffectState.Nada)
                return true;
            else
                return false;
        }

        public void SetRoomState(RoomEffectState effectState, float resetTime, bool sendData) {
            roomState = effectState;
            resetIn = resetTime;
            onTimer = true;
            if(sendData && !DevBoy.yes)
                SendData();
        }

        public void Cancel() {
            if(resetters.Count != 0) {
                foreach (IAbilityResetter reset in resetters)
                    reset.ResetEffect();
            }
            roomState = RoomEffectState.Nada;
            onTimer = false;
        }

        private class RoomIdExtraction
        {
            public int myId;
            public float timerTime;
            public int roomState;
        }

        [System.Serializable]
        private class RoomStateData
        {
            public int roomState;
            public int myId;
            public float timerTime;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public RoomStateData(int roomState, int myId, float time) {
                this.roomState = roomState;
                this.myId = myId;
                this.timerTime = time;
                eventName = "roomStateSync";
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}