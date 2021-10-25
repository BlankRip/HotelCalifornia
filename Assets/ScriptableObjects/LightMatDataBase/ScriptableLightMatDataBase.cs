using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    [CreateAssetMenu()]
    public class ScriptableLightMatDataBase : ScriptableObject
    {
        [SerializeField] List<LightMatData> lightMats;
        private Dictionary<LightColour, Material> dictionary;

        public Material GetMaterial(LightColour type) {
            SetUpNecessaryData();
            return dictionary[type];
        }

        private void SetUpNecessaryData() {
            if(dictionary == null) {
                dictionary = new Dictionary<LightColour, Material>();
                foreach (LightMatData matData in lightMats)
                    dictionary.Add(matData.type, matData.material);
            }
        }

        [System.Serializable]
        private class LightMatData {
            public LightColour type;
            public Material material;
        }
    }

}