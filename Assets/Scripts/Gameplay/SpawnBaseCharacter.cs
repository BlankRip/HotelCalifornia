using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.Gameplay {
    public class SpawnBaseCharacter : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterDatad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] GameObject ghost;
        [SerializeField] GameObject human;

        private void Awake() {
            if(characterDatad.characterType == CharacterType.Ghost)
                GameObject.Instantiate(ghost, spawnPoint.position, spawnPoint.rotation);
            if(characterDatad.characterType == CharacterType.Human)
                GameObject.Instantiate(human, spawnPoint.position, spawnPoint.rotation);
            
            Destroy(this.gameObject, 0.3f);
        }
    }
}