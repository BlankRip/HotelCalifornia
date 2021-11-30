using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddlerInputPanel : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] ScriptablePlayerController playerController;
        [SerializeField] ScriptablePlayerCamera playerCamera;
        [SerializeField] TMP_InputField inputField;
        [SerializeField] Button lockInButton;
        private RiddleSolutionPad currentPad;
        private RiddleBoard currentBoard;
        
        private int ridlesToSolve = 3;
        private int solvedSoFar;
        private bool solved;

        public void OpenPanel(RiddleSolutionPad pad) {
            currentPad = pad;
            lockInButton.onClick.AddListener(SolutionInput);
            OpenPanel();
        }

        public void OpenPanel(RiddleBoard board) {
            currentBoard = board;
            lockInButton.onClick.AddListener(InterfereInput);
            OpenPanel();
        }

        private void OpenPanel() {
            playerController.controller.LockControls(true);
            playerCamera.cam.Lock(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            panel.SetActive(true);
        }

        private void SolutionInput() {
            bool solvedThis = currentPad.Check(inputField.text);
            if(solvedThis)
                UpdateBoardSolve();
            lockInButton.onClick.RemoveListener(SolutionInput);
            ClosePanel();
            AudioPlayer.instance.PlayAudio2DOneShot(ClipName.Numpad);
        }

        private void UpdateBoardSolve() {
            solvedSoFar++;
            if(solvedSoFar == ridlesToSolve)
                solved = true;
        }

        private void InterfereInput() {
            currentBoard.ChangeText(inputField.text);
            lockInButton.onClick.RemoveListener(InterfereInput);
            ClosePanel();
        }

        private void ClosePanel() {
            if(!solved) {
                playerController.controller.LockControls(false);
                playerCamera.cam.Lock(false);
            }
            inputField.text = "";
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            currentPad = null;
            currentBoard = null;
            panel.SetActive(false);
        }
    }
}