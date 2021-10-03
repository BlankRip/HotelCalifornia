using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    [CreateAssetMenu()]
    public class ScriptablePortionMatDataBase : ScriptableObject
    {
        public List<PortionMatData> portionMats;
        private Dictionary<PortionType, Material> dictionary;

        public Material GetMaterial(PortionType type) {
            SetUpNecessaryData();
            return dictionary[type];
        }

        private void SetUpNecessaryData() {
            if(dictionary == null) {
                dictionary = new Dictionary<PortionType, Material>();
                foreach (PortionMatData matData in portionMats)
                    dictionary.Add(matData.type, matData.material);
            }
        }
    }

    [System.Serializable]
    public class PortionMatData {
        public PortionType type;
        public Material material;
    }
}