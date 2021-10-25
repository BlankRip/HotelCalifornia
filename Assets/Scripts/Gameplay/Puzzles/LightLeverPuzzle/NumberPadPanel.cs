using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class NumberPadPanel : MonoBehaviour
    {
        [SerializeField] ScriptablePlayerController playerController;
        [SerializeField] ScriptablePlayerCamera playerCamera;
        [SerializeField] GameObject panel;
        [SerializeField] TextMeshProUGUI myText;
        private NumberPad numberPad;
        private int currentIndex;
        private List<int> inputs;

        private void Start() {
            inputs = new List<int>{-1, -1, -1, -1};
        }

        public void OpenPanel(NumberPad pad) {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            numberPad = pad;
            playerController.controller.LockControls(true);
            playerCamera.cam.Lock(true);
            panel.SetActive(true);
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
            numberPad.CheckSolution(inputs);
            ClearInputs();
            ClosePanel();
        }

        private void ClearInputs() {
            for (int i = 0; i < inputs.Count; i++)
                inputs[i] = -1;
            currentIndex = 0;
            UpdateUi();
        }

        private void ClosePanel() {
            playerController.controller.LockControls(false);
            playerCamera.cam.Lock(false);
            numberPad = null;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            panel.SetActive(false);
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