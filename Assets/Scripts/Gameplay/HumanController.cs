using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class HumanController : MonoBehaviour, IPlayerController
    {
        [SerializeField] ScriptablePlayerController currentController;
        private IPlayerMovement movement;

        private IAbility primary;
        private IAbility secondary;

        private IInteractRay interactRay;

        private float horizontalInput;
        private float verticalInput;
        private bool jump;
        private bool crouch;

        private void Awake() {
            currentController.controller = this;
        }

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
            interactRay = GetComponent<IInteractRay>();
        }

        private void Update() {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            if(Input.GetKeyDown(KeyCode.Space))
                jump = true;
            if(Input.GetKeyDown(KeyCode.LeftControl))
                crouch = true;
            else if(Input.GetKeyUp(KeyCode.LeftControl))
                crouch = false;

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

        private void FixedUpdate() {
            movement.Move(horizontalInput, verticalInput, ref jump, ref crouch);
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
    }
}