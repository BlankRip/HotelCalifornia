using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class SpawnBaseCharacter : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterDatad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] string ghost;
        [SerializeField] string human;

        private void Awake() {
            if(characterDatad.characterType == CharacterType.Ghost) {
                NetConnector.instance.SendDataToServer(
                    JsonUtility.ToJson(
                        new SpawnObject(ghost, spawnPoint.position, spawnPoint.rotation)
                    )
                );
            }
            if(characterDatad.characterType == CharacterType.Human) {
                NetConnector.instance.SendDataToServer(
                    JsonUtility.ToJson(
                        new SpawnObject(human, spawnPoint.position, spawnPoint.rotation)
                    )
                );
            }
            
            Destroy(this.gameObject, 0.3f);
        }
    }
}