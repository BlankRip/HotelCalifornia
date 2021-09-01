using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.CharacterSelect {
    public class AbilitySelect : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI descripsionText;
        private AbilitySelectEvents currentSelected;
        private string selectedDescripsion;

        public void SwitchSelected(string description, AbilitySelectEvents events) {
            selectedDescripsion = description;
            currentSelected?.DeselectButton();
            currentSelected = events;
            currentSelected.SelectButton();
        }

        public void ChangeDescripsion(string description) {
            descripsionText.text = description;
        }

        public void SelectedDescripsion() {
            descripsionText.text = selectedDescripsion;
        }
    }
}