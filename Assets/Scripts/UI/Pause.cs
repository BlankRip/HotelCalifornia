using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Inputs;

namespace Knotgames.UI.Pause {
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;
        [SerializeField] ScriptableUiInputs inputs;
        
        private void Start() {
            inputs.uiInputs.SetOnEscapeEvent(() => ShowPause());
        }

        public void ShowPause() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
            ActivateUiInputs();
        }

        private void ActivateUiInputs() {
            inputs.uiInputs.SwitchToUiInputs();
            inputs.uiInputs.SetOnEscapeEvent(() => HidePause());
        }

        public void HidePause() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            DeActivateUiInputs();
        }

        public void DeActivateUiInputs() {
            inputs.uiInputs.SwitchToGamePlayInputs();
            inputs.uiInputs.SetOnEscapeEvent(() => ShowPause());
        }
    }
}