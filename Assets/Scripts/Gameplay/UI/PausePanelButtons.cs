using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Knotgames.UI;

namespace Knotgames.Gameplay.UI {
    public class PausePanelButtons : MonoBehaviour
    {
        [Header("If you want to automate button Events")]
        [SerializeField] GameObject resumeButton;
        [SerializeField] GameObject backToMenuButton;
        private Pause pause;

        private void Start() {
            pause = FindObjectOfType<Pause>();
            resumeButton?.GetComponent<IMenuButton>().SetOnClick(() => Resume());
            backToMenuButton?.GetComponent<IMenuButton>().SetOnClick(() => BackToMainMenu());
        }

        public void Resume() {
            pause.HidePause();
        }

        public void BackToMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}