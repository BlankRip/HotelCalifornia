using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.CharacterSelect {
    public class AbilitySelectEvents : MonoBehaviour
    {
        [SerializeField] bool baseSelected;
        [SerializeField] AbilitySelect selector;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] AbilityType myAbilityType;
        [SerializeField] GameObject selectedOverlay;
        [SerializeField] int myIndex;
        public Sprite icone;
        public string abilityName;
        [TextArea(5, 5)]
        [SerializeField] string descripsion;

        private void Start() {
            if(baseSelected)
                OnClick();
        }

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