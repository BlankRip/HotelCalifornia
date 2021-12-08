using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using Knotgames.Audio;
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
        [SerializeField] Collider exitTrigger;
        [SerializeField] GameObject exitViaual;
        private int solvesNeeded = 3;
        private int puzzlesSolved;
        private Vector3 returnPos;
        private Quaternion returnRot;
        private int playersExited;
        private int playerNeededToEndGame = 2;

        private void Awake() {
            puzzlesSolved = 0;
            puzzleStatus.tracker = this;
        }

        public void OnePuzzleSolved() {
            puzzlesSolved++;
            DoorVisualUpdate(puzzlesSolved);
        }

        private void DoorVisualUpdate(int solves) {
            LockThings();
            StartCoroutine(ActivateLight(solves - 1));
        }

        private void LockThings() {
            controller.controller.LockControls(true);
            cam.cam.Lock(true);
            returnPos = cam.cam.GetTransform().position;
            returnRot = cam.cam.GetTransform().rotation;
        }

        private IEnumerator ActivateLight(int lightIndex) {
            MoveCamToDoorView();
            AudioPlayer.instance.PlayAudio3D(ClipName.Solved, transform.position);
            yield return new WaitForSeconds(1f);
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
            exitViaual.SetActive(true);
            GetComponent<Animator>().SetBool("open", true);
            exitTrigger.enabled = true;
            widthAdjusterObj.SetActive(true);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Human")) {
                InterationRay isLocal = other.GetComponent<InterationRay>();
                other.gameObject.SetActive(false);
                if(isLocal != null) {
                    LockThings();
                    MoveCamToDoorView();
                }
                playersExited++;
                if(playersExited == playerNeededToEndGame) {
                    if(isLocal != null && !DevBoy.yes) {
                        Debug.Log("Net send human Win");
                        NetGameManager.instance.ToggleWinScreen(true);
                    }
                    Debug.Log("Humans Win");
                }
            }
        }
    }
}