using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class HumanAbilityEffectTrigger : MonoBehaviour, IAbilityEffectTrigger
    {
        //^IAbilityEffect testEffect;
        [SerializeField] NetObject netObj;
        private NetSendData dataToSend;

        private bool underEffect;
        private float timer;
        private bool onTimer;
        private IAbilityEffect currentEffect;

        private IAbilityEffect blurEffect;
        private IAbilityEffect protectionEffect;


        private void Start() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            dataToSend = new NetSendData(netObj.id, 0, 0);
            
            blurEffect = GetComponent<BlurEffect>();
            netObj.OnMessageRecieve += RecieveData;
        }

        private void OnDestroy() {
            netObj.OnMessageRecieve -= RecieveData;
        }

        private void SendData(int effectType, float duration) {
            dataToSend.effectType = effectType;
            dataToSend.duration = duration;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void RecieveData(string recieved) {
            if(JsonUtility.FromJson<ObjectNetData>(recieved).componentType == "EffectTrigger") {
                DataExtraction extracted = JsonUtility.FromJson<DataExtraction>(recieved);
                TriggerEffect((AbilityEffectType)extracted.effectType, extracted.duration, false);
            }
        }

        private void Update() {
            if(onTimer) {
                timer -= Time.deltaTime;
                if(timer <= 0) {
                    if((DevBoy.yes || netObj.IsMine) && currentEffect != null)
                        currentEffect.ResetEffect();
                    underEffect = false;
                    onTimer = false;
                    currentEffect = null;
                }
            }
        }

        public void TriggerEffect(AbilityEffectType type, float duration, bool sendData) {
            if(DevBoy.yes || netObj.IsMine) {
                switch(type) {
                    case AbilityEffectType.BlurEffect:
                        blurEffect.ApplyEffect();
                        currentEffect = blurEffect;
                        break;
                    case AbilityEffectType.HumanProtection:
                        break;
                }
            }

            if(sendData)
                SendData((int) type, duration);

            Debug.LogError("Triggred");

            underEffect = true;
            timer = duration;
            onTimer = true;
        }

        public bool IsUnderEffect() {
            return underEffect;
        }

        private class DataExtraction 
        {
            public int effectType;
            public float duration;
        }

        [System.Serializable]
        private class NetSendData
        {
            public string eventName;
            public string componentType;
            public string distributionOption;
            //public string ownerID;
            public string objectID;
            public string roomID;
            public int effectType;
            public float duration;

            public NetSendData(string netId, int effectType, float duration) {
                eventName = "syncObjectData";
                distributionOption = DistributionOption.serveOthers;
                componentType = "EffectTrigger";
                if(!DevBoy.yes)
                {
                    //ownerID = NetConnector.instance.playerID.value;
                    roomID = NetRoomJoin.instance.roomID.value;
                }
                objectID = netId;
            }
        }
    }
}