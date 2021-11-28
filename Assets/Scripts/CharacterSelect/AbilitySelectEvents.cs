using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Knotgames.CharacterData;

namespace Knotgames.CharacterSelect {
    public class AbilitySelectEvents : MonoBehaviour
    {
        [SerializeField] bool baseSelected;
        [SerializeField] AbilitySelect selector;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] AbilityType myAbilityType;
        [SerializeField] Image myAbilityImage;
        [SerializeField] GameObject selectedOverlay;
        [SerializeField] int myIndex;

        public AbilityUiData myData;

        private void Start() {
            myData = abilityUiCollection.GetAbilityData(myAbilityType);
            myAbilityImage.sprite = myData.abilitySprite;
            if(baseSelected)
                OnClick();
        }

        public void OnEnter() {
            selector.ChangeDescripsion(myData.description);
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
            selector.SwitchSelected(myData.description, this);
        }
    }
}