using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class GhostController : MonoBehaviour
    {
        IPlayerMovement movement;

        private float horizontalInput;
        private float verticalInput;
        private bool levitateUp;
        private bool levitateDown;

        private void Start() {
            movement = GetComponent<IPlayerMovement>();
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
            
            movement.Move(horizontalInput, verticalInput, ref levitateUp, ref levitateDown);
        }
    }
}