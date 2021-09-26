using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay
{
    public class TriggerState : MonoBehaviour, ITriggerState
    {
        private static int currentId;
        [SerializeField] bool startTrigger;
        private TriggerEffectState triggerState;
        private List<IAbilityResetter> resetters;
        private float resetIn;
        private bool onTimer;
        private int id;
        private TriggerStateData dataToSend;

        private void Awake()
        {
            if (startTrigger)
                currentId = 0;
            id = currentId;
            currentId++;
            dataToSend = new TriggerStateData((int)triggerState, id, 0);
            if (!DevBoy.yes)
                NetUnityEvents.instance.roomTiggerOnMsgRecieve.AddListener(ReadData);
            resetters = new List<IAbilityResetter>();
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.roomTiggerOnMsgRecieve.RemoveListener(ReadData);
        }

        private void SendData()
        {
            dataToSend.triggerState = (int)triggerState;
            dataToSend.timerTime = resetIn;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void ReadData(string recieved)
        {
            TriggerIdExtraction check = JsonUtility.FromJson<TriggerIdExtraction>(recieved);
            if (id == check.myId)
                SetTriggerState((TriggerEffectState)check.triggerState, check.timerTime, false);
        }

        private void Update()
        {
            if (onTimer)
            {
                resetIn -= Time.deltaTime;
                if (resetIn <= 0)
                {
                    if (resetters.Count != 0)
                    {
                        foreach (IAbilityResetter reset in resetters)
                            reset.ResetEffect();
                    }
                    triggerState = TriggerEffectState.Nada;
                    onTimer = false;
                }
            }
        }

        public TriggerEffectState GetTriggerState()
        {
            return triggerState;
        }

        public void SetTriggerState(TriggerEffectState effectState, float resetTime, bool sendData)
        {
            triggerState = effectState;
            resetIn = resetTime;
            onTimer = true;
            Debug.LogError($"TIMER OF {resetTime} STARTED");
            if (sendData && !DevBoy.yes)
                SendData();
        }

        private class TriggerIdExtraction
        {
            public int myId;
            public float timerTime;
            public int triggerState;
        }

        [System.Serializable]
        private class TriggerStateData
        {
            public int triggerState;
            public int myId;
            public float timerTime;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public TriggerStateData(int triggerState, int myId, float time)
            {
                this.triggerState = triggerState;
                this.myId = myId;
                this.timerTime = time;
                eventName = "triggerStateSync";
                distributionOption = DistributionOption.serveOthers;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}