using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Inputs;
using Knotgames.Events;

namespace Knotgames.UI.Pause {
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;
        [SerializeField] ScriptableInputManager inputs;
        [SerializeField] ScriptableUIEvents uiEvents;
        [SerializeField] ScriptableGameplayEvents gameplayEvents;
        
        private void Start() {
            gameplayEvents.onEscape.AddListener(ShowPause);
        }

        public void ShowPause() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
            ActivateUiInputs();
        }

        private void ActivateUiInputs() {
            inputs.inputManager.SwapToUI();
            uiEvents.onEscape.AddListener(HidePause);
        }

        public void HidePause() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            DeActivateUiInputs();
        }

        public void DeActivateUiInputs() {
            inputs.inputManager.SwapToGameplay();
            uiEvents.onEscape.RemoveListener(HidePause);
        }
    }
}