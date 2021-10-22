using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Abilities {
    public class HumanAbilityEffectTrigger : MonoBehaviour, IAbilityEffectTrigger
    {
        [SerializeField] NetObject netObj;
        private NetSendData dataToSend;

        private bool underEffect;
        private float timer;
        private bool onTimer;
        private IAbilityEffect currentEffect;

        private IAbilityEffect blurEffect;
        private IAbilityEffect protectionEffect;
        private IAbilityEffect teleportEffect;
        private IAbilityEffect delusionalEffect;
        private IHumanMoveAdjustment humanMoveAdjustment;


        private void Start() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            dataToSend = new NetSendData(netObj.id, 0, 0);
            
            humanMoveAdjustment = GetComponent<IHumanMoveAdjustment>();
            blurEffect = GetComponent<BlurEffect>();
            teleportEffect = GetComponent<TeleportEffect>();
            delusionalEffect = GetComponent<DelusionalEffect>();
            netObj.OnMessageRecieve += RecieveData;
        }

        private void OnDestroy() {
            netObj.OnMessageRecieve -= RecieveData;
        }

        private void SendData(int effectType, float duration, bool masterOnly) {
            dataToSend.effectType = effectType;
            dataToSend.duration = duration;
            dataToSend.masterOnly = masterOnly;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void RecieveData(string recieved) {
            if(JsonUtility.FromJson<ObjectNetData>(recieved).componentType == "EffectTrigger") {
                DataExtraction extracted = JsonUtility.FromJson<DataExtraction>(recieved);
                TriggerEffect((AbilityEffectType)extracted.effectType, extracted.duration, extracted.masterOnly, false);
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

        public void TriggerEffect(AbilityEffectType type, float duration, bool masterOnly, bool sendData) {
            if(sendData && !DevBoy.yes)
                SendData((int) type, duration, masterOnly);

            Debug.LogError("Triggred");

            underEffect = true;
            timer = duration;
            onTimer = true;

            if(!DevBoy.yes) {
                if(masterOnly) {
                    if(!netObj.IsMine) {
                        switch(type) {
                            case AbilityEffectType.Teleport:
                                humanMoveAdjustment.InvokeTpEvent();
                                break;
                        }
                        return;
                    }
                }
            }

            switch(type) {
                case AbilityEffectType.BlurEffect:
                    blurEffect.ApplyEffect();
                    currentEffect = blurEffect;
                    break;
                case AbilityEffectType.HumanProtection:
                    break;
                case AbilityEffectType.Teleport:
                    teleportEffect.ApplyEffect();
                    currentEffect = teleportEffect;
                    break;
                case AbilityEffectType.Delusional:
                    delusionalEffect.ApplyEffect();
                    currentEffect = delusionalEffect;
                    break;
            }
        }

        public bool IsUnderEffect() {
            return underEffect;
        }

        private class DataExtraction 
        {
            public int effectType;
            public float duration;
            public bool masterOnly;
        }

        [System.Serializable]
        private class NetSendData
        {
            public string eventName;
            public string componentType;
            public string distributionOption;
            public string objectID;
            public string roomID;
            public int effectType;
            public float duration;
            public bool masterOnly;

            public NetSendData(string netId, int effectType, float duration) {
                eventName = "syncObjectData";
                distributionOption = DistributionOption.serveOthers;
                componentType = "EffectTrigger";
                if(!DevBoy.yes) {
                    roomID = NetRoomJoin.instance.roomID.value;
                }
                objectID = netId;
            }
        }
    }
}