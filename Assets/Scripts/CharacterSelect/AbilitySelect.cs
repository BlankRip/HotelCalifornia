using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Knotgames.CharacterSelect {
    public class AbilitySelect : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI descripsionText;
        [SerializeField] TextMeshProUGUI selectedTitle;
        [SerializeField] TextMeshProUGUI selectedDescripsionText;
        [SerializeField] Image icone;
        private AbilitySelectEvents currentSelected;
        private string selectedDescripsion;

        public void SwitchSelected(string description, AbilitySelectEvents events) {
            selectedDescripsion = description;
            currentSelected?.DeselectButton();
            currentSelected = events;
            currentSelected.SelectButton();
            selectedDescripsionText.text = selectedDescripsion;
            selectedTitle.text = currentSelected.myData.abilityName;
            icone.sprite = currentSelected.myData.abilitySprite;
        }

        public void ChangeDescripsion(string description) {
            descripsionText.text = description;
        }

        public void SelectedDescripsion() {
            descripsionText.text = selectedDescripsion;
        }
    }
}