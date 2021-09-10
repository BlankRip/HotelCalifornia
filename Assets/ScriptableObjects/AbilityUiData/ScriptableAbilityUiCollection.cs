using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.CharacterData {
    [CreateAssetMenu()]
    public class ScriptableAbilityUiCollection : ScriptableObject
    {
        [SerializeField] List<AbilityUiData> allData;
        public AbilityUiData GetAbilityData(AbilityType type) {
            for(int i = 0; i < allData.Count; i++) {
                if(type == allData[i].type)
                    return allData[i];
            }
            return null;
        }
    }

    [System.Serializable]
    public class AbilityUiData {
        public AbilityType type;
        public Sprite abilitySprite;
        public int baseUses;
    }
}