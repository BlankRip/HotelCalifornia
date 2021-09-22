using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay {
    public class HumanController : MonoBehaviour, IPlayerController
    {
        [SerializeField] ScriptablePlayerController currentController;
        [SerializeField] NetObject netObj;
        private IPlayerMovement movement;
        private IPlayerAnimator animator;

        private IAbility primary;
        private IAbility secondary;
        System.Action RunAgain;
        private IInteractRay interactRay;

        //Y Positive is Jump & Y Negetive is Crouch
        private PlayerNetData data;

        private void Awake() {
            currentController.controller = this;
        }

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
            animator = GetComponent<IPlayerAnimator>();
            interactRay = GetComponent<IInteractRay>();
            if(netObj == null)
                netObj = GetComponent<NetObject>();
            if(!DevBoy.yes) {
                data = new PlayerNetData(netObj.id);
                // netObj.OnMessageRecieve += RecieveNetData;
                // SendNetData();
            } else
                data = new PlayerNetData(netObj.id);
                
            StartCoroutine(CallMe());
        }

        // private void SendNetData() {
        //     if(netObj.IsMine) {
        //         NetConnector.instance.SendDataToServer(JsonUtility.ToJson(data));
        //         Invoke("SendNetData", 0.2f);
        //     }
        // }

        // private void RecieveNetData(string revieved) {
        //     RunAgain = () =>
        //     {
        //         if(!netObj.IsMine) {
        //             switch(JsonUtility.FromJson<ObjectNetData>(revieved).componentType) {
        //                 case "PlayerNetData":
        //                     data = JsonUtility.FromJson<PlayerNetData>(revieved);
        //                     Debug.LogError($"Hor: {data.horizontalInput} \n Vert: {data.verticalInput} \n Jum: {data.moveYPositive}");
        //                     break;
        //             }
        //         }
        //     };
        // }

        private void Update() {
            if(DevBoy.yes || netObj.IsMine) {
                data.horizontalInput = Input.GetAxisRaw("Horizontal");
                data.verticalInput = Input.GetAxisRaw("Vertical");
                if(Input.GetKeyDown(KeyCode.Space))
                    data.moveYPositive = true;
                if(Input.GetKeyDown(KeyCode.LeftControl))
                    data.moveYNegetive = true;
                else if(Input.GetKeyUp(KeyCode.LeftControl))
                    data.moveYNegetive = false;
                if(Input.GetKeyDown(KeyCode.E)) {
                    if(primary.CanUse())
                        primary.UseAbility();
                }
                if(Input.GetKeyDown(KeyCode.Q)) {
                    if(secondary.CanUse())
                        secondary.UseAbility();
                }
                if(Input.GetKeyDown(KeyCode.F)) {
                    if(interactRay.CanInteract())
                        interactRay.Interact();
                }
            }
            movement.Move(data.horizontalInput, data.verticalInput, ref data.moveYPositive, ref data.moveYNegetive);
            animator.Animate(data.horizontalInput, data.verticalInput, data.moveYPositive, data.moveYNegetive);
        }

        public void SetAbilities(List<IAbility> abilities) {
            primary = abilities[0];
            secondary = abilities[1];
        }

        public GameObject GetPlayerObject() {
            return this.gameObject;
        }

        public void SwapSecondary(IAbility ability) {
            secondary.Destroy();
            secondary = ability;
        }

        IEnumerator CallMe()
        {
            yield return new WaitForSeconds(0.5f);
            if (RunAgain != null)
                RunAgain.Invoke();
            StartCoroutine(CallMe());
        }
    }
}