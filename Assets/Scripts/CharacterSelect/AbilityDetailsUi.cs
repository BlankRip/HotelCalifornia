using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Knotgames.CharacterData;

namespace Knotgames.UI {
    public class AbilityDetailsUi : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] Image myIcon;

        public void SetData(AbilityUiData data) {
            titleText.text = data.abilityName;
            descriptionText.text = data.description;
            myIcon.sprite = data.abilitySprite;
        }
    }
}