using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Abilities {
    public class MovementTrapObj : MonoBehaviour, IMovementTrap
    {
        [SerializeField] NetObject netObj;
        [SerializeField] ScriptableMoveTrap moveTrap;
        [SerializeField] Transform trapProjection;
        [SerializeField] Transform nutrelizerProjection;
        [SerializeField] Transform trap;
        [SerializeField] Transform nutrelizer;
        private float distance;
        private float maxDistance = 20;

        private void Start() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            netObj.OnMessageRecieve += RecieveData;

            if(DevBoy.yes || netObj.IsMine) {
                trapProjection.gameObject.SetActive(true);
                nutrelizerProjection.gameObject.SetActive(true);
                maxDistance = maxDistance * maxDistance;
                moveTrap.trap = this;
            }
        }

        private void OnDestroy() {
            netObj.OnMessageRecieve -= RecieveData;
        }

        private void RecieveData(string recieved) {
            if(JsonUtility.FromJson<ObjectNetData>(recieved).componentType == "MoveTrap") {
                DataExtraction extracted = JsonUtility.FromJson<DataExtraction>(recieved);
                SetTrap(extracted.trapPos, extracted.nutralizerPos, extracted.nutralizerRot.ToQuaternion());
            }
        }

        public void MoveTrapTo(Vector3 position) {
            trapProjection.position = position;
        }

        public void MoveNutralizerTo(Vector3 postion, Vector3 surfaceNormal) {
            distance = (postion - trapProjection.position).sqrMagnitude;
            if(distance <= maxDistance) {
                nutrelizerProjection.position = postion;
                nutrelizerProjection.up = surfaceNormal;
            }
        }

        public void SetTrap() {
            SetTrap(trapProjection.position, nutrelizerProjection.position, nutrelizerProjection.rotation);
            SendTrapData();
        }

        private void SetTrap(Vector3 trapPos, Vector3 nutralizerPos, Quaternion nutralizerRot) {
            trap.position = trapPos;
            nutrelizer.position = nutralizerPos;
            nutrelizer.rotation = nutralizerRot;

            trapProjection.gameObject.SetActive(false);
            nutrelizerProjection.gameObject.SetActive(false);
            trap.gameObject.SetActive(true);
            nutrelizer.gameObject.SetActive(true);
        }

        private void SendTrapData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new NetSendData(netObj.id, trapProjection.position, 
                nutrelizerProjection.position, new RotationWS(nutrelizerProjection.rotation))));
        }

        public void DestroyTrap() {
            moveTrap.trap = null;
            if(DevBoy.yes)
                Destroy(this.gameObject);
            else
                netObj.DeleteObject();
        }

        private class DataExtraction 
        {
            public Vector3 trapPos;
            public Vector3 nutralizerPos;
            public RotationWS nutralizerRot;
        }

        [System.Serializable]
        private class NetSendData
        {
            public string eventName;
            public string componentType;
            public string distributionOption;
            public string objectID;
            public string roomID;
            public Vector3 trapPos;
            public Vector3 nutralizerPos;
            public RotationWS nutralizerRot;

            public NetSendData(string netId, Vector3 trapPos, Vector3 nutralizerPos, RotationWS nutralizerRot) {
                eventName = "syncObjectData";
                distributionOption = DistributionOption.serveOthers;
                componentType = "MoveTrap";
                if(!DevBoy.yes) {
                    roomID = NetRoomJoin.instance.roomID.value;
                }
                objectID = netId;
                this.trapPos = trapPos;
                this.nutralizerPos = nutralizerPos;
                this.nutralizerRot = nutralizerRot;
            }
        }
    }
}