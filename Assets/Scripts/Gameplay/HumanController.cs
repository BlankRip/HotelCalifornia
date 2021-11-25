using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.Gameplay.Abilities;
using Knotgames.Audio;

namespace Knotgames.Gameplay {
    public class HumanController : MonoBehaviour, IPlayerController
    {
        [SerializeField] ScriptablePlayerController currentController;
        [SerializeField] NetObject netObj;
        private IPlayerMovement movement;
        private IPlayerAnimator animator;

        private IAbility primary;
        private IAbility ultimate;
        private IInteractRay interactRay;

        //Y Positive is Jump & Y Negetive is Crouch
        private PlayerNetData data;
        private bool lockedController;

        private void Awake() {
        }

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
            animator = GetComponent<IPlayerAnimator>();
            interactRay = GetComponent<IInteractRay>();
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            if(!DevBoy.yes) {
                data = new PlayerNetData(netObj.id);
                netObj.OnMessageRecieve += RecieveNetData;
                SendNetDataRepeat();
            } else
                data = new PlayerNetData(netObj.id);

            if(DevBoy.yes || netObj.IsMine)
                currentController.controller = this;
        }

        private void OnDestroy() {
            netObj.OnMessageRecieve -= RecieveNetData;
        }

        private void SendNetDataRepeat() {
            if(netObj.IsMine) {
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(data));
                Invoke("SendNetDataRepeat", 0.2f);
            }
        }

        private void SendNetData() {
            if(!DevBoy.yes && netObj.IsMine)
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(data));
        }

        private void RecieveNetData(string revieved) {
            if(!netObj.IsMine) {
                switch(JsonUtility.FromJson<ObjectNetData>(revieved).componentType) {
                    case "PlayerNetData":
                        data = JsonUtility.FromJson<PlayerNetData>(revieved);
                        break;
                }
            }
        }

        private void Update() {
            if(DevBoy.yes || netObj.IsMine) {
                if(!lockedController) {
                    data.horizontalInput = Input.GetAxisRaw("Horizontal");
                    data.verticalInput = Input.GetAxisRaw("Vertical");
                    if(Input.GetKeyDown(KeyCode.Space)) {
                        data.moveYPositive = true;
                        SendNetData();
                        StopAllCoroutines();
                        StartCoroutine(SendAfter3Frames());
                    }
                    if(Input.GetKeyDown(KeyCode.LeftControl))
                        data.moveYNegetive = true;
                    else if(Input.GetKeyUp(KeyCode.LeftControl))
                        data.moveYNegetive = false;
                    if(Input.GetKeyDown(KeyCode.E)) {
                        if(primary.CanUse()) {
                                primary.UseAbility();
                                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.CastAbility);
                            }
                    }
                    if(Input.GetKeyDown(KeyCode.Q)) {
                        if(ultimate.CanUse()) {
                                ultimate.UseAbility();
                                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.CastAbility);
                            }
                    }
                    if(Input.GetKeyDown(KeyCode.Mouse0)) {
                        if(interactRay.CanInteract())
                            interactRay.Interact();
                    }
                }
                movement.Move(data.horizontalInput, data.verticalInput, ref data.moveYPositive, ref data.moveYNegetive);
            }

            animator.Animate(data.horizontalInput, data.verticalInput, data.moveYPositive, data.moveYNegetive);
        }

        public void SetAbilities(List<IAbility> abilities) {
            primary = abilities[0];
            ultimate = abilities[1];
        }

        public GameObject GetPlayerObject() {
            return this.gameObject;
        }

        public void SwapSecondary(IAbility ability) {
            if(ultimate != null)
                ultimate.Destroy();
            ultimate = ability;
        }

        IEnumerator SendAfter3Frames()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            SendNetData();
        }

        public void LockControls(bool lockState) {
            lockedController = lockState;
            if(lockedController) {
                Debug.Log("LOCKED");
                data.horizontalInput = 0;
                data.verticalInput = 0;
                data.moveYNegetive = data.moveYPositive = false;
            }
        }
    }
}