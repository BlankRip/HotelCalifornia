using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay {
    public class HumanAbilitySwaper : MonoBehaviour
    {
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] List<AbilityType> giveableAbility;
        [SerializeField] ScriptablePlayerController player;
        private IAbilityUi secondarySlot;

        private void Start() {
            secondarySlot = GameObject.FindGameObjectWithTag("SecondaryUi").GetComponent<IAbilityUi>();
        }

        //! FOR TESTING ONLY
        private void Update() {
            if(Input.GetKeyDown(KeyCode.K))
                Swap();
        }

        public void Swap() {
            int rand = Random.Range(0, giveableAbility.Count);
            player.controller.SwapSecondary(AttachAbility(giveableAbility[rand]));
            AbilityUiData uiData = abilityUiCollection.GetAbilityData(giveableAbility[rand]);
            secondarySlot.UpdateObjectData(uiData.baseUses, uiData.abilitySprite);
        }

        private IAbility AttachAbility(AbilityType type) {
            switch(type) {
                case AbilityType.NullAbilityRoom:
                    return player.controller.GetPlayerObject().AddComponent<TestAbility2>();
                case AbilityType.test3:
                    return player.controller.GetPlayerObject().AddComponent<TestAbility2>();
                case AbilityType.test4:
                    return player.controller.GetPlayerObject().AddComponent<TestAbility2>();
                default:
                    return player.controller.GetPlayerObject().AddComponent<DummyAbility>();
            }
        }
    }
}