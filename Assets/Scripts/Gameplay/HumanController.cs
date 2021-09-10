using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class HumanController : MonoBehaviour
    {
        IPlayerMovement movement;

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
            if(Input.GetKeyUp(KeyCode.LeftControl))
                crouch = false;
            
            movement.Move(horizontalInput, verticalInput, ref jump, ref crouch);
        }
    }
}