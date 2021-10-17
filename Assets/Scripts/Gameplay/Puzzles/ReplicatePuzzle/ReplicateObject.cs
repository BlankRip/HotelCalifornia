using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateObject : MonoBehaviour, IReplicateObject, IInteractable
    {
        private static int portionIds;
        public static void ResetIds()
        {
            portionIds = 0;
        }
        Transform attachPos;
        bool held = false;
        private bool inUse;
        private int myId;
        private ILocalNetTransformSync transformSync;
        private DataToSend dataToSend;
        Rigidbody rb;
        [HideInInspector] public bool slotted;
        [HideInInspector] public ReplicateObjectSlot mySlot;

        private void Awake()
        {
            myId = portionIds;
            portionIds++;
            transformSync = GetComponent<ILocalNetTransformSync>();
            transformSync.SetID(myId);

            if (!DevBoy.yes)
                NetUnityEvents.instance.portionUseStatus.AddListener(RecieveData);
        }

        private void Start()
        {
            dataToSend = new DataToSend(myId);

            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            rb = GetComponent<Rigidbody>();
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.portionUseStatus.RemoveListener(RecieveData);
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.myId == myId)
            {
                inUse = extracted.inUse;
                if (inUse)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
                else
                {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                }
            }
        }

        private void SendInUseData(bool value)
        {
            dataToSend.inUse = value;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        public void Interact()
        {
            if (!inUse)
            {
                if (!held)
                    Pick();
                else
                    Drop();
            }
        }

        public void Drop()
        {
            Debug.LogError("DROPPED");
            if (!DevBoy.yes)
            {
                transformSync.SetDataSyncStatus(false);
                SendInUseData(false);
            }

            held = false;
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void Pick()
        {
            if (!DevBoy.yes)
            {
                transformSync.SetDataSyncStatus(true);
                SendInUseData(true);
            }
            if (slotted)
            {
                slotted = false;
                mySlot.myObj = null;
                Invoke("SlotReset", 0.2f);
            }

            held = true;
            transform.SetParent(attachPos);
            transform.localPosition = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        void SlotReset()
        {
            mySlot.myCollider.enabled = true;
        }

        public void HideInteractInstruction() { }

        public void ShowInteractInstruction() { }

        private class ExtractionClass
        {
            public int myId;
            public bool inUse;
        }

        private class DataToSend
        {
            public int myId;
            public bool inUse;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id)
            {
                eventName = "repObjUseStatus";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}