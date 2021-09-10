using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.UI {
    public class AbilityUiObject : MonoBehaviour, IAbilityUi
    {
        [SerializeField] GameObject content;
        [SerializeField] Image abilityImage;
        [SerializeField] GameObject disabledOverlay;
        [SerializeField] TextMeshProUGUI useleftText;

        public GameObject GetGameObject() {
            return content;
        }

        public void UpdateObjectData(int usesLeft, Sprite image) {
            abilityImage.sprite = image;
            UpdateObjectData(usesLeft);
        }

        public void UpdateObjectData(int usesLeft) {
            useleftText.text = usesLeft.ToString();
            if(usesLeft == 0)
                disabledOverlay.SetActive(true);
            else
                disabledOverlay.SetActive(false);
            
            if(content.activeSelf == false)
                content.SetActive(true);
        }
    }
}