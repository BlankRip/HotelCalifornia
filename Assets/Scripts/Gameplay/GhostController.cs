using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class GhostController : MonoBehaviour, IPlayerController
    {
        [SerializeField] ScriptablePlayerController currentController;
        private IPlayerMovement movement;

        private IAbility primary;
        private IAbility secondary;
        private IAbility ultimate;

        private IInteractRay interactRay;

        private float horizontalInput;
        private float verticalInput;
        private bool levitateUp;
        private bool levitateDown;

        private void Awake() {
            currentController.controller = this;
        }

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
            interactRay = GetComponent<IInteractRay>();
        }

        private void Update() {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            if(Input.GetKeyDown(KeyCode.Space))
                levitateUp = true;
            else if(Input.GetKeyUp(KeyCode.Space))
                levitateUp = false;

            if(Input.GetKeyDown(KeyCode.LeftControl))
                levitateDown = true;
            else if(Input.GetKeyUp(KeyCode.LeftControl))
                levitateDown = false;

            if(Input.GetKeyDown(KeyCode.E)) {
                if(primary.CanUse())
                    primary.UseAbility();
            }
            if(Input.GetKeyDown(KeyCode.Q)) {
                if(secondary.CanUse())
                    secondary.UseAbility();
            }
            if(Input.GetKeyDown(KeyCode.R)) {
                if(ultimate.CanUse())
                    ultimate.UseAbility();
            }

            if(Input.GetKeyDown(KeyCode.F)) {
                if(interactRay.CanInteract())
                    interactRay.Interact();
            }
        }

        private void FixedUpdate() {
            movement.Move(horizontalInput, verticalInput, ref levitateUp, ref levitateDown);
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
    }
}