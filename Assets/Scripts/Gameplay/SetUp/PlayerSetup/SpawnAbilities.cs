using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Network;

namespace Knotgames.Gameplay.Abilities {
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
                case AbilityType.ClearTraps:
                    return gameObject.AddComponent<ClearTrapsTrigger>();
                case AbilityType.TeleportHuman:
                    return gameObject.AddComponent<TeleportHumanTrigger>();
                case AbilityType.SelfProtect:
                    return gameObject.AddComponent<SelfProtectTrigger>();
                case AbilityType.BlurVision:
                    return gameObject.AddComponent<BlurTrigger>();
                case AbilityType.BanishGhost:
                    return gameObject.AddComponent<BanishGhostTrigger>();
                case AbilityType.Delusional:
                    return gameObject.AddComponent<DelusionalTrigger>();
                default:
                    return gameObject.AddComponent<DummyAbility>();
            }
        }
    }
}