using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle {
    public class SolutionPad : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI myText;
        [SerializeField] Transform targetPos;

        private void Update() {
            myText.transform.position = targetPos.position;
            myText.transform.rotation = targetPos.rotation;
        }

        public void UpdateText(string textValue) {
            myText.text = textValue;

            // myText.transform.position = targetPos.position;
            // myText.transform.rotation = targetPos.rotation;

            this.gameObject.SetActive(true);
            myText.gameObject.SetActive(true);
        }
    }
}