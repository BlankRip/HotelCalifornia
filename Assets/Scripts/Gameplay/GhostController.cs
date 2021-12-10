using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.Gameplay.Abilities;
using Knotgames.Audio;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay {
    public class GhostController : MonoBehaviour, IPlayerController, IGhostControlerAdjustment
    {
        [SerializeField] ScriptablePlayerController currentController;
        [SerializeField] NetObject netObj;
        private IPlayerMovement movement;
        private IPlayerAnimator animator;

        private IAbility primary;
        private IAbility secondary;
        private IAbility ultimate;

        // Y Positive is Levitate up & Y Negetive is Levitate Down
        private PlayerNetData data;

        private bool checkAbilityInputs;
        private bool lockedController;

        private void Awake() {
            currentController.controller = this;
        }

        private void Start() {
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            movement = GetComponent<IPlayerMovement>();
            animator = GetComponent<IPlayerAnimator>();
            checkAbilityInputs = true;

            if(!DevBoy.yes) {
                data = new PlayerNetData(netObj.id);
                SendNetData();
                netObj.OnMessageRecieve += RecieveNetData;
            } else {
                data = new PlayerNetData(netObj.id);
            }

            if(DevBoy.yes || netObj.IsMine)
                currentController.controller = this;
        }

        private void SendNetData() {
            if(netObj.IsMine) {
                NetConnector.instance.SendDataToServer(JsonUtility.ToJson(data));
                Invoke("SendNetData", 0.2f);
            }
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
                    data.horizontalInput = Input.GetAxis("Horizontal");
                    data.verticalInput = Input.GetAxis("Vertical");
                    if(Input.GetKeyDown(KeyCode.Space))
                        data.moveYPositive = true;
                    else if(Input.GetKeyUp(KeyCode.Space))
                        data.moveYPositive = false;

                    if(Input.GetKeyDown(KeyCode.LeftControl))
                        data.moveYNegetive = true;
                    else if(Input.GetKeyUp(KeyCode.LeftControl))
                        data.moveYNegetive = false;

                    if(Input.GetKeyDown(KeyCode.E)) {
                        if(checkAbilityInputs) {
                            if(primary.CanUse()) {
                                primary.UseAbility();
                                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.CastAbility);
                            }
                        } else
                            InstructionText.instance.ShowInstruction("Currently Abilities Locked In This Room");
                    }
                    if(Input.GetKeyDown(KeyCode.F)) {
                        if(checkAbilityInputs) {
                            if(secondary.CanUse()) {
                                secondary.UseAbility();
                                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.CastAbility);
                            }
                        } else
                            InstructionText.instance.ShowInstruction("Currently Abilities Locked In This Room");
                    }
                    if(Input.GetKeyDown(KeyCode.Q)) {
                        if(checkAbilityInputs) {
                            if(ultimate.CanUse()) {
                                ultimate.UseAbility();
                                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.CastAbility);
                            }
                        } else
                            InstructionText.instance.ShowInstruction("Currently Abilities Locked In This Room");
                    }
                }
                // if(Input.GetKeyDown(KeyCode.Mouse0)) {
                //     if(interactRay.CanInteract())
                //         interactRay.Interact();
                // }
                movement.Move(data.horizontalInput, data.verticalInput, ref data.moveYPositive, ref data.moveYNegetive);
            }

            animator.Animate(data.horizontalInput, data.verticalInput, data.moveYPositive, data.moveYNegetive);
        }

        public void SetAbilities(List<IAbility> abilities) {
            primary = abilities[0];
            secondary = abilities[1];
            ultimate = abilities[2];
        }

        public GameObject GetPlayerObject() {
            return this.gameObject;
        }

        public void SwapSecondary(IAbility ability) {
            throw new System.NotImplementedException();
        }

        public void SetAbilityUsability(bool value) {
            checkAbilityInputs = value;
        }

        public void LockControls(bool lockState) {
            lockedController = lockState;
            if(lockState) {
                data.horizontalInput = 0;
                data.verticalInput = 0;
                data.moveYNegetive = data.moveYPositive = false;
            }
        }
    }
}