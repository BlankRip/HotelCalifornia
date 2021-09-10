using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class HumanController : MonoBehaviour, IPlayerControler
    {
        private IPlayerMovement movement;

        private IAbility primary;
        private IAbility secondary;

        private float horizontalInput;
        private float verticalInput;
        private bool jump;
        private bool crouch;

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
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
        }

        private void FixedUpdate() {
            movement.Move(horizontalInput, verticalInput, ref jump, ref crouch);
        }

        public void SetAbilities(List<IAbility> abilities) {
            primary = abilities[0];
            secondary = abilities[1];
        }
    }
}