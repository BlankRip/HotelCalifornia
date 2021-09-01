using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.CharacterSelect {
    public class AbilitySelectEvents : MonoBehaviour
    {
        [SerializeField] AbilitySelect selector;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] AbilityType myAbilityType;
        [SerializeField] GameObject selectedOverlay;
        [SerializeField] int myIndex;
        [TextArea(5, 5)]
        [SerializeField] string descripsion;

        public void OnEnter() {
            selector.ChangeDescripsion(descripsion);
        }

        public void OnExit() {
            selector.SelectedDescripsion();
        }

        public void SelectButton() {
            selectedOverlay.SetActive(true);
        }

        public void DeselectButton() {
            selectedOverlay.SetActive(false);
        }

        public void OnClick() {
            characterData.abilityTypes[myIndex] = myAbilityType;
            selector.SwitchSelected(descripsion, this);
        }
    }
}