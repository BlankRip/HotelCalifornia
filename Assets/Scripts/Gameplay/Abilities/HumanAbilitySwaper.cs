using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class HumanAbilitySwaper : MonoBehaviour
    {
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] List<AbilityType> giveableAbility;
        [SerializeField] ScriptablePlayerController player;
        private IAbilityUi secondarySlot;
        int abilityIndex;

        private void Start() {
            secondarySlot = GameObject.FindGameObjectWithTag("SecondaryUi").GetComponent<IAbilityUi>();
            abilityIndex = Random.Range(0, giveableAbility.Count);
        }

        //!TODO FOR TESTING ONLY
        private void Update() {
            if(Input.GetKeyDown(KeyCode.K))
                Swap();
        }

        public void Swap() {
            player.controller.SwapSecondary(AttachAbility(giveableAbility[abilityIndex]));
            AbilityUiData uiData = abilityUiCollection.GetAbilityData(giveableAbility[abilityIndex]);
            secondarySlot.UpdateObjectData(uiData.baseUses, uiData.abilitySprite);
            Debug.Log(giveableAbility[abilityIndex]);
        }

        private IAbility AttachAbility(AbilityType type) {
            switch(type) {
                case AbilityType.BanishGhost:
                    return player.controller.GetPlayerObject().AddComponent<BanishGhostTrigger>();
                case AbilityType.NoEntryRoom:
                    return player.controller.GetPlayerObject().AddComponent<NoEntryTrigger>();
                default:
                    return player.controller.GetPlayerObject().AddComponent<DummyAbility>();
            }
        }
    }
}