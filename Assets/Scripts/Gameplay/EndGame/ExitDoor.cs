using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class ExitDoor : MonoBehaviour, IPuzzleStatusTracker
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleStatus;
        [SerializeField] ScriptablePlayerCamera cam;
        [SerializeField] ScriptablePlayerController controller;
        [SerializeField] Transform camPosition;
        [SerializeField] List<GameObject> solvedLights;
        [SerializeField] GameObject widthAdjusterObj;
        private int solvesNeeded = 3;
        private int puzzlesSolved;
        private Vector3 returnPos;
        private Quaternion returnRot;

        private void Awake() {
            puzzlesSolved = 0;
            puzzleStatus.tracker = this;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Y))
                OnePuzzleSolved();
        }

        public void OnePuzzleSolved() {
            puzzlesSolved++;
            DoorVisualUpdate(puzzlesSolved);
        }

        private void DoorVisualUpdate(int solves) {
            LockThings();
            StartCoroutine(ActivateLight(solves - 1));
            //* Turn on ligts or somehting to indicate the number of puzzles solved
        }

        private void LockThings() {
            controller.controller.LockControls(true);
            cam.cam.Lock(true);
            returnPos = cam.cam.GetTransform().position;
            returnRot = cam.cam.GetTransform().rotation;
        }

        private IEnumerator ActivateLight(int lightIndex) {
            MoveCamToDoorView();
            yield return new WaitForSeconds(1f);
            Debug.Log("Here");
            solvedLights[lightIndex].SetActive(true);
            yield return new WaitForSeconds(1);
            if(puzzlesSolved == solvesNeeded) {
                OpenDoor();
                yield return new WaitForSeconds(1);
            }
            UnlockThings();
        }

        private void MoveCamToDoorView() {
            cam.cam.GetTransform().position = camPosition.position;
            cam.cam.GetTransform().rotation = camPosition.rotation;
        }

        private void UnlockThings() {
            cam.cam.GetTransform().position = returnPos;
            cam.cam.GetTransform().rotation = returnRot;
            cam.cam.Lock(false);
            controller.controller.LockControls(false);
        }

        private void OpenDoor() {
            //* Open Door Here
            GetComponent<Animator>().SetBool("open", true);
            widthAdjusterObj.SetActive(true);
        }
    }
}