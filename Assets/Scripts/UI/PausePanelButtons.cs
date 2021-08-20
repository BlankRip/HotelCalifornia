using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Knotgames.UI.Pause {
    public class PausePanelButtons : MonoBehaviour
    {
        [Header("If you want to automate button Events")]
        [SerializeField] Button resumeButton;
        [SerializeField] Button backToMenuButton;
        private Pause pause;

        private void Start() {
            pause = FindObjectOfType<Pause>();

            resumeButton?.onClick.AddListener(() => Resume());
            backToMenuButton?.onClick.AddListener(() => BackToMainMenu());
        }

        public void Resume() {
            pause.HidePause();
        }

        public void BackToMainMenu() {
            SceneManager.LoadScene(0);
        }
    }
}