using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.UI;

namespace Knotgames.Gameplay.UI {
    public class PausePanelButtons : MonoBehaviour
    {
        [Header("If you want to automate button Events")]
        [SerializeField] bool useAutoAssign;
        [SerializeField] GameObject resumeButton;
        [SerializeField] GameObject backToMenuButton;
        private Pause pause;

        private void Start() {
            pause = FindObjectOfType<Pause>();
            if(useAutoAssign) {
                resumeButton.GetComponent<IMenuButton>().SetOnClick(() => Resume());
                backToMenuButton.GetComponent<IMenuButton>().SetOnClick(() => BackToMainMenu());
            }
        }

        public void Resume() {
            pause.HidePause();
        }

        public void BackToMainMenu() {
            SceneManager.LoadScene(0);
        }

        public void ControlScreenVisibility(bool activate) {
            pause.ToggleControlsScreen(activate);
        }

        public void SettingsScreenVisibility(bool activate) {
            pause.ToggleSettingsScreen(activate);
        }
    }
}