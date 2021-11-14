using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class StartMixer : MonoBehaviour, IInteractable, IInterfear
    {
        private IMixer mixer;
        private bool useable;
        private float interfearDisableTime = 5f;
        private DataToSend startData;
        private DataToSend usableData;

        private void Start() {
            mixer = GetComponentInParent<IMixer>();
            useable = true;

            startData = new DataToSend("startMixer");
            usableData = new DataToSend("interfearMixer");
            if(!DevBoy.yes)
                NetUnityEvents.instance.mixerEvents.AddListener(RecieveData);
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.mixerEvents.RemoveListener(RecieveData);
        }

        private void SendInterfearData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(usableData));
        }

        private void SendStartMixerData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(startData));
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.eventName == "startMixer") {
                Debug.Log("Starting Mixer");
                mixer.StartMix();
            }
            else if(extracted.eventName == "interfearMixer") {
                useable = false;
                Invoke("MakeUsable", interfearDisableTime);
            }
        }

        public void Interact() {
            if(useable) {
                mixer.StartMix();
                if(!DevBoy.yes)
                    SendStartMixerData();
            }
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        public void ShowInteractInstruction() {
            if(useable)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Start Mixer");
        }

        public void Interfear() {
            if(!DevBoy.yes)
                SendInterfearData();

            useable = false;
            Invoke("MakeUsable", interfearDisableTime);
        }
        
        private void MakeUsable() {
            useable = true;
        }

        public bool CanInterfear() {
            return useable;
        }

        private class ExtractionClass {
            public string eventName;
        }

        private class DataToSend {
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(string eventName) {
                this.eventName = eventName;
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}