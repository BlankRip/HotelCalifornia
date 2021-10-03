using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.CharacterData {
    [CreateAssetMenu()]
    public class ScriptableSpawnDataCollection : ScriptableObject {
        public SpawnData human1Data;
        public SpawnData human2Data;
        public SpawnData human3Data;
        public SpawnData human4Data;
        public SpawnData human5Data;
        public SpawnData human6Data;
        public SpawnData ghost1Data;
        public SpawnData ghost2Data;
        public SpawnData ghost3Data;
    }

    [System.Serializable]
    public class SpawnData {
        public GameObject model;
        public Vector3 localSpawnOffset;
        public bool animatorOnModel;
        public Avatar animationAvatar;
        public RuntimeAnimatorController animatorController;
    }
}