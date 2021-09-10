using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.Gameplay.UI {
    public class AbilityUiSpawner : MonoBehaviour
    {
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] GameObject primary;
        [SerializeField] GameObject secondary;
        [SerializeField] GameObject ultimate;
        private List<IAbilityUi> uiObjects;

        private void Awake() {
            FillUiObjectList();
            SpawnAbilityUI();
            Destroy(this, 0.2f);
        }

        private void FillUiObjectList() {
            uiObjects = new List<IAbilityUi>();
            uiObjects.Add(primary.GetComponent<IAbilityUi>());
            uiObjects.Add(secondary.GetComponent<IAbilityUi>());
            uiObjects.Add(ultimate.GetComponent<IAbilityUi>());
        }

        private void SpawnAbilityUI() {
            for (int i = 0; i < characterData.abilityTypes.Count; i++) {
                if(characterData.abilityTypes[i] != AbilityType.Nada) {
                    AbilityUiData data = abilityUiCollection.GetAbilityData(characterData.abilityTypes[i]);
                    uiObjects[i].UpdateObjectData(data.baseUses, data.abilitySprite);
                } else 
                    uiObjects[i].GetGameObject().SetActive(false);
            }
        }
    }
}
