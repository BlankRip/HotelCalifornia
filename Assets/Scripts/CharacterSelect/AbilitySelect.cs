using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.UI;

namespace Knotgames.CharacterSelect {
    public class AbilitySelect : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI descripsionText;
        [SerializeField] AbilityDetailsUi myDetailsUi;
        private AbilitySelectEvents currentSelected;
        private string selectedDescripsion;

        public void SwitchSelected(string description, AbilitySelectEvents events) {
            selectedDescripsion = description;
            currentSelected?.DeselectButton();
            currentSelected = events;
            currentSelected.SelectButton();
            myDetailsUi.SetData(currentSelected.myData);
        }

        public void ChangeDescripsion(string description) {
            descripsionText.text = description;
        }

        public void SelectedDescripsion() {
            descripsionText.text = selectedDescripsion;
        }
    }
}