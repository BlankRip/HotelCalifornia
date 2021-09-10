using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.CharacterData {
    [CreateAssetMenu()]
    public class ScriptableSpawnDataCollection : ScriptableObject {
        public SpawnData human1Data;
        public SpawnData human2Data;
    }

    [System.Serializable]
    public class SpawnData {
        public GameObject model;
        public Vector3 localSpawnOffset;
        public Avatar animationAvatar;
        public RuntimeAnimatorController animatorController;
    }
}