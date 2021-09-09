using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.CharacterSelect {
    public class SpawnCharacter : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] SpawnData human1Data;

        private void Awake() {
            switch(characterData.modelType) {
                case ModelType.Human1:
                    Spawn(human1Data);
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
            Animator animator = GetComponent<Animator>();
            animator.avatar = data.animationAvatar;
            animator.runtimeAnimatorController = data.animatorController;
            Destroy(this, 0.2f);
        }
    }

    [System.Serializable]
    public class SpawnData {
        public GameObject model;
        public Vector3 localSpawnOffset;
        public Avatar animationAvatar;
        public RuntimeAnimatorController animatorController;
    }
}