using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    [CreateAssetMenu()]
    public class ScriptableLightMatDataBase : ScriptableObject
    {
        [SerializeField] List<LightMatData> lightMats;
        private Dictionary<LightColor, Material> dictionary;

        public Material GetMaterial(LightColor type) {
            SetUpNecessaryData();
            return dictionary[type];
        }

        private void SetUpNecessaryData() {
            if(dictionary == null) {
                dictionary = new Dictionary<LightColor, Material>();
                foreach (LightMatData matData in lightMats)
                    dictionary.Add(matData.type, matData.material);
            }
        }

        [System.Serializable]
        private class LightMatData {
            public LightColor type;
            public Material material;
        }
    }

}