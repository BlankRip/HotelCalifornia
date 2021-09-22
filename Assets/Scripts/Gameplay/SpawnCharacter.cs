using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class SpawnCharacter : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] ScriptableSpawnDataCollection allSpawnData;
        [SerializeField] NetObject netObj;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
        }

        private void Start() {
            if(!DevBoy.yes)
                netObj.OnMessageRecieve += RecieveNetData;
            if(DevBoy.yes || netObj.IsMine) {
                SpawnSelect();
                if(!DevBoy.yes)
                    SendData();
            }
        }

        private void SendData() {
            if(netObj.IsMine)
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ModelSpawnNetData(characterData.modelType, netObj.id)));
        }

        private void RecieveNetData(string recieved) {
            if(!netObj.IsMine) {
                switch(JsonUtility.FromJson<ObjectNetData>(recieved).componentType) {
                    case "ModelSpawnNetData":
                        ModelType type = JsonUtility.FromJson<ModelSpawnNetData>(recieved).modelType;
                        //SpawnSelect(type);
                        Spawn(allSpawnData.human1Data);
                        break;
                }
            }
        }

        private void SpawnSelect(ModelType type = ModelType.Nada) {
            if(type == ModelType.Nada)
                type = characterData.modelType;
            switch(type) {
                case ModelType.Human1:
                    Spawn(allSpawnData.human1Data);
                    return;
                case ModelType.Human2:
                    Spawn(allSpawnData.human2Data);
                    return;
                case ModelType.Human3:
                    Spawn(allSpawnData.human3Data);
                    return;
                case ModelType.Human4:
                    Spawn(allSpawnData.human4Data);
                    return;
                case ModelType.Ghost1:
                    Spawn(allSpawnData.human1Data);
                    return;
                case ModelType.Ghost2:
                    Spawn(allSpawnData.human2Data);
                    return;
                default:
                    return;
            }
        }

        private void Spawn(SpawnData data) {
            GameObject model = GameObject.Instantiate(data.model, Vector3.zero, Quaternion.identity);
            model.transform.parent = this.transform;
            model.transform.localPosition = Vector3.zero;
            model.transform.localPosition += data.localSpawnOffset;
            model.transform.localRotation = Quaternion.identity;
            // Animator animator = GetComponent<Animator>();
            // animator.avatar = data.animationAvatar;
            // animator.runtimeAnimatorController = data.animatorController;
            Destroy(this, 0.2f);
        }
    }
}