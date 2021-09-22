using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class SpawnAbilities : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] NetObject netObj;
        private IPlayerController controler;
        private List<IAbility> abilities;

        private void Awake() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();

            if(DevBoy.yes || netObj.IsMine) {
                abilities = new List<IAbility>();
                for (int i = 0; i < characterData.abilityTypes.Count; i++) {
                    abilities.Add(AttachAbility(characterData.abilityTypes[i]));
                }
                controler = GetComponent<IPlayerController>();
                controler.SetAbilities(abilities);
            }
            Destroy(this, 0.2f);
        }

        private IAbility AttachAbility(AbilityType type) {
            switch(type) {
                case AbilityType.test1:
                    return gameObject.AddComponent<TestAbility>();
                case AbilityType.test2:
                    return gameObject.AddComponent<TestAbility2>();
                case AbilityType.test3:
                    return gameObject.AddComponent<TestAbility2>();
                case AbilityType.test4:
                    return gameObject.AddComponent<TestAbility2>();
                case AbilityType.test5:
                    return gameObject.AddComponent<TestAbility3>();
                case AbilityType.test6:
                    return gameObject.AddComponent<TestAbility3>();
                default:
                    return gameObject.AddComponent<DummyAbility>();
            }
        }
    }
}