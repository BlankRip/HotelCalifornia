using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class PortionObj : MonoBehaviour, IPortion, IInteractable
    {
        private static int portionIds;
        public static void ResetIds() {
            portionIds = 0;
        }

        [SerializeField] ScriptablePortionMatDataBase matDataBase;
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] List<PortionType> baseSpawnableTypes;
        [SerializeField] GameObject liquidMat;
        private PortionType myType;
        private Renderer liquidRenderer;
        private bool typeSet;
        private int portionTypeLastIndex;

        Transform attachPos;
        bool held = false;
        Rigidbody rb;
        private Vector3 restPos;
        private IMixerSlot mySlot;

        private int myId;
        private ILocalNetTransformSync transformSync;
        private bool inUse;
        private DataToSend dataToSend;

        private void Awake() {
            myId = portionIds;
            portionIds++;
            transformSync = GetComponent<ILocalNetTransformSync>();
            transformSync.SetID(myId);

            if(!DevBoy.yes)
                NetUnityEvents.instance.portionUseStatus.AddListener(RecieveData);
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId) {
                inUse = extracted.inUse;
                if(inUse) {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                } else {
                    rb.useGravity = true;
                    rb.isKinematic = false;
                }
            }
        }

        private void SendInUseData(bool value) {
            dataToSend.inUse = value;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void Start() {
            dataToSend = new DataToSend(myId);

            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            rb = GetComponent<Rigidbody>();
            liquidRenderer = liquidMat.GetComponent<Renderer>();

            CullSpawnableList();
            if(baseSpawnableTypes.Count > 0) {
                int rand = Random.Range(0, baseSpawnableTypes.Count);
                SetPortionType(baseSpawnableTypes[rand]);
            }
            restPos = transform.position;
            portionTypeLastIndex = System.Enum.GetValues(typeof(PortionType)).Length - 1;

            eventCollection.twistVision.AddListener(TwistVision);
            eventCollection.fixVision.AddListener(BackToNormalVision);
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.portionUseStatus.RemoveListener(RecieveData);
            
            eventCollection.twistVision.RemoveListener(TwistVision);
            eventCollection.fixVision.RemoveListener(BackToNormalVision);
        }
        
        public bool GetUseState() {
            return inUse;
        }

        private void TwistVision() {
            int currentValue = (int)myType;
            if(currentValue == portionTypeLastIndex)
                ChangeMat((PortionType)0);
            else
                ChangeMat((PortionType)(currentValue+1));
        }

        private void BackToNormalVision() {
            ChangeMat(myType);
        }

        private void ChangeMat(PortionType type) {
            liquidRenderer.material = matDataBase.GetMaterial(type);
        }

        private void CullSpawnableList() {
            List<PortionType> spawnedList = chemRoom.manager.GetSpawnedTypes();
            foreach (PortionType type in spawnedList) {
                if(baseSpawnableTypes.Contains(type))
                    baseSpawnableTypes.Remove(type);
            }
        }

        public PortionType GetPortionType() {
            return myType;
        }

        public void SetPortionType(PortionType type) {
            if(!typeSet) {
                myType = type;
                if(liquidRenderer!=null)
                    liquidRenderer.material = matDataBase.GetMaterial(myType);
                else 
                {
                    liquidRenderer = liquidMat.GetComponent<Renderer>();
                    liquidRenderer.material = matDataBase.GetMaterial(myType);
                }
                chemRoom.manager.AddToSpawnedList(type);
                typeSet = true;
            }
        }

        public void HideInteractInstruction() {}

        public void ShowInteractInstruction() {}

        public void Interact()
        {
            if(!inUse) {
                if (!held)
                    Pick();
                else
                    Drop();
            }
        }

        public void Drop()
        {
            Debug.LogError("DROPPED");
            if(!DevBoy.yes) {
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
            if(!DevBoy.yes) {
                transformSync.SetDataSyncStatus(true);
                SendInUseData(true);
            }

            if(mySlot != null) {
                if(mySlot.CanReturn()) {
                    mySlot.ReturingFromSlot();
                    mySlot = null;
                } else return;
            }
            restPos = transform.position;
            held = true;
            transform.SetParent(attachPos);
            transform.localPosition = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("GhostEdge"))
                transform.position = restPos;
        }

        public void SetMySlot(IMixerSlot mySlot) {
            this.mySlot = mySlot;
    
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
                eventName = "potionUseStatus";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }

}