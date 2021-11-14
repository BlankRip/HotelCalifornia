using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

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
        private HideStatus hideStatus;
        Rigidbody rb;
        private Vector3 restPos;
        [HideInInspector] public bool slotted;
        [HideInInspector] public IReplicateSlot mySlot;
        [SerializeField] string myName;
        MeshRenderer[] renderers;

        private void Awake()
        {
            myId = portionIds;
            portionIds++;
            transformSync = GetComponent<ILocalNetTransformSync>();
            transformSync.SetID(myId);
            rb = GetComponent<Rigidbody>();
            renderers = GetComponentsInChildren<MeshRenderer>();

            if (!DevBoy.yes)
                NetUnityEvents.instance.repObjUseStatus.AddListener(RecieveData);
        }

        private void Start()
        {
            dataToSend = new DataToSend(myId);
            hideStatus = new HideStatus();

            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            restPos = transform.position;
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.repObjUseStatus.RemoveListener(RecieveData);
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.myId == myId)
            {
                inUse = extracted.inUse;
                slotted = extracted.slotted;
                if (inUse)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
                else
                {
                    if (!slotted)
                    {
                        rb.useGravity = true;
                        rb.isKinematic = false;
                    }
                }
            }
        }

        private void SendInUseData(bool value, bool slotValue)
        {
            dataToSend.inUse = value;
            dataToSend.slotted = slotValue;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void SendHideStatus(bool value)
        {
            hideStatus.status = value;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(hideStatus));
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
            if (!DevBoy.yes)
            {
                transformSync.SetDataSyncStatus(false);
                SendInUseData(false, false);
                SendHideStatus(false);
            }

            held = false;
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void Drop(bool overrider, Transform t)
        {
            if (!DevBoy.yes)
            {
                transformSync.SetDataSyncStatus(false);
                SendInUseData(false, true);
                SendHideStatus(false);
            }

            held = false;
            transform.SetParent(null);
            transform.SetPositionAndRotation(t.position, t.rotation);
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public void Pick()
        {
            if (!DevBoy.yes)
            {
                transformSync.SetDataSyncStatus(true);
                SendInUseData(true, false);
                SendHideStatus(true);
            }
            if (slotted)
            {
                slotted = false;
                mySlot.ClearSlot();
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
            mySlot.SetCollider(true);
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        public void ShowInteractInstruction() {
            if(!inUse)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Interact With Object");
        }

        public string GetName()
        {
            return myName;
        }

        public void HandleSlotting(IReplicateSlot slot, Vector3 position, Quaternion rotation)
        {
            slotted = true;
            mySlot = slot;
            transform.SetPositionAndRotation(position, rotation);
        }

        public void Disable()
        {
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("GhostEdge"))
                transform.position = restPos;
        }

        private class ExtractionClass
        {
            public int myId;
            public bool inUse;
            public bool slotted;
        }

        private class DataToSend
        {
            public int myId;
            public bool inUse;
            public bool slotted;
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

        private class HideStatus
        {
            public bool status;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public HideStatus()
            {
                eventName = "slotHideStatus";
                distributionOption = DistributionOption.serveMe;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}