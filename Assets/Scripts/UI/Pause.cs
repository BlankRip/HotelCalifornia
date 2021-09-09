using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Inputs;
using Knotgames.Events;

namespace Knotgames.UI.Pause {
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;
        bool showing;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(pausePanel.activeSelf == false)
                    ShowPause();
            }
            if(showing) {
                if(pausePanel.activeSelf == false)
                    HidePause();
            }
        }

        public void ShowPause() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showing = true;
            pausePanel.SetActive(true);
        }

        public void HidePause() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}