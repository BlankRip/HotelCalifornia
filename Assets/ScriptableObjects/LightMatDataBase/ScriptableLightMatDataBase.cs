using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    [CreateAssetMenu()]
    public class ScriptableLightMatDataBase : ScriptableObject
    {
        [SerializeField] List<LightMatData> lightMats;
        private Dictionary<LightColor, GameObject> dictionary;

        public GameObject GetMaterial(LightColor type) {
            SetUpNecessaryData();
            return dictionary[type];
        }

        private void SetUpNecessaryData() {
            if(dictionary == null) {
                dictionary = new Dictionary<LightColor, GameObject>();
                foreach (LightMatData matData in lightMats)
                    dictionary.Add(matData.type, matData.lightEffectPrefab);
            }
        }

        [System.Serializable]
        private class LightMatData {
            public LightColor type;
            public Material material;
            public GameObject lightEffectPrefab;
        }
    }

}