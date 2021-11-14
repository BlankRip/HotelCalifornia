using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class DeliveryItem : MonoBehaviour, IInteractable
    {
        private static int deliveryId;
        public static void RestIDs() {
            deliveryId = 0;
        }

        private Transform attachPos;
        public bool localHeld;
        private bool inUse;

        private int myId;
        private DataToSend dataToSend;
        private ILocalNetTransformSync transformSync;

        private void Awake() {
            myId = deliveryId;
            deliveryId++;
            if(myId >= 3)
                Debug.Log(myId);

            dataToSend = new DataToSend(myId);
            transformSync = GetComponent<ILocalNetTransformSync>();
            transformSync.SetID(myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.deliveryUseStatus.AddListener(RecieveData);
        }

        private void Start() {
            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId)
                inUse = extracted.inUse;
        }

        private void SendInUseData(bool value) {
            dataToSend.inUse = value;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.deliveryUseStatus.RemoveListener(RecieveData);
        }

        public void Interact() {
            if(!inUse) {
                if (!localHeld)
                    Pick();
                else
                    Drop();
            }
        }

        private void Pick() {
            if(!DevBoy.yes) {
                transformSync.SetDataSyncStatus(true);
                SendInUseData(true);
            }

            localHeld = true;
            transform.SetParent(attachPos);
            transform.localPosition = Vector3.zero;
        }

        private void Drop() {
            if(!DevBoy.yes) {
                transformSync.SetDataSyncStatus(false);
                SendInUseData(false);
            }

            localHeld = false;
            transform.SetParent(null);
        }

        public void ShowInteractInstruction() {
            if(!inUse)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Interact With Delivery Item");
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        private class ExtractionClass {
            public int myId;
            public bool inUse;
        }

        private class DataToSend {
            public int myId;
            public bool inUse;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "delivaryUseStatus";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}