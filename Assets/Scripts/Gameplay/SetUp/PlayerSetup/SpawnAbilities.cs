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

        private void Start() {
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
                case AbilityType.SlowRoom:
                    return gameObject.AddComponent<SlowRoomTrigger>();
                case AbilityType.NullAbilityRoom:
                    return gameObject.AddComponent<NullAbilityTrigger>();
                case AbilityType.NoEntryRoom:
                    return gameObject.AddComponent<NoEntryTrigger>();
                case AbilityType.test4:
                    return gameObject.AddComponent<TestAbility2>();
                case AbilityType.test5:
                    return gameObject.AddComponent<TestAbility3>();
                case AbilityType.SelfProtect:
                    return gameObject.AddComponent<SelfProtectTrigger>();
                case AbilityType.BlurVision:
                    return gameObject.AddComponent<BlurTrigger>();
                default:
                    return gameObject.AddComponent<DummyAbility>();
            }
        }
    }
}