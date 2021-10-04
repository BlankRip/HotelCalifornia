using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Puzzle {
    public class SolutionPad : MonoBehaviour
    {
        [SerializeField] Transform targetPos;
        private TextMeshProUGUI myText;

        // private void Update() {
        //     myText.transform.position = targetPos.position;
        //     myText.transform.rotation = targetPos.rotation;
        // }

        public void UpdateText(string textValue) {
            myText = ObjectPool.instance.SpawnPoolObj("ChemPadText", targetPos.position, targetPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = textValue;

            this.gameObject.SetActive(true);
            myText.gameObject.SetActive(true);
        }
    }
}