using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class NumberPadPanel : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI myText;
        private int currentIndex;
        private List<int> inputs;

        private void Start() {
            inputs = new List<int>{-1, -1, -1, -1};
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        public void ButtonPressed(int buttonValue) {
            if(currentIndex != 4) {
                inputs[currentIndex] = buttonValue;
                currentIndex++;
                UpdateUi();
            }
        }

        public void DeleteInput() {
            if(currentIndex != 0) {
                currentIndex--;
                inputs[currentIndex] = -1;
                UpdateUi();
            }
        }

        public void Submit() {

        }

        private void UpdateUi() {
            string value = "";
            for (int i = 0; i < inputs.Count; i++) {
                if(inputs[i] == -1)
                    value += "- ";
                else
                 value += $"{inputs[i]} ";
            }
            myText.text = value;
        }
    }
}