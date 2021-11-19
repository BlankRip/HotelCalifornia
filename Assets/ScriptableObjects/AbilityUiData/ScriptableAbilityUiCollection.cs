using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.CharacterData {
    [CreateAssetMenu()]
    public class ScriptableAbilityUiCollection : ScriptableObject
    {
        [SerializeField] List<AbilityUiData> allData;
        private Dictionary<AbilityType, AbilityUiData> dataDictionary;

        public AbilityUiData GetAbilityData(AbilityType type) {
            FillDictionary();
            if(dataDictionary.ContainsKey(type))
                return dataDictionary[type];
            return null;
        }

        private void FillDictionary() {
            if(dataDictionary == null || dataDictionary.Count != allData.Count) {
                dataDictionary = new Dictionary<AbilityType, AbilityUiData>();
                for(int i = 0; i < allData.Count; i++)
                    dataDictionary.Add(allData[i].type, allData[i]);
            }
        }
    }

    [System.Serializable]
    public class AbilityUiData {
        public AbilityType type;
        public string abilityName;
        [TextArea(5, 5)]
        public string description;
        public Sprite abilitySprite;
        public int baseUses;
    }
}