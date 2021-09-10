using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.Gameplay {
    public class SpawnAbilities : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterData;
        private IPlayerControler controler;
        private List<IAbility> abilities;

        private void Awake() {
            abilities = new List<IAbility>();
            for (int i = 0; i < characterData.abilityTypes.Count; i++) {
                if(characterData.abilityTypes[i] != AbilityType.Nada)
                    abilities.Add(AttachAbility(characterData.abilityTypes[i]));
            }
            controler = GetComponent<IPlayerControler>();
            controler.SetAbilities(abilities);
            Destroy(this, 0.2f);
        }

        private IAbility AttachAbility(AbilityType type) {
            switch(type) {
                case AbilityType.test1:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test2:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test3:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test4:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test5:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test6:
                    return gameObject.AddComponent<TestAbility>();
                default:
                    return gameObject.AddComponent<TestAbility>();
            }
        }
    }
}