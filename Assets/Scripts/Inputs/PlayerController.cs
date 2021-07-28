using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RVD
{
    public class PlayerController : InputController
    {
        public void Update()
        {
            Movement();

            Debug.Log(controller.isGrounded);
            if (jump)
            {
                if (controller.isGrounded)
                    verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
                jump = false;
            }

            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            playerCam.eulerAngles = targetRotation;
        }

        public override void OnLowerFinish(InputValue value)
        {
            crouch = false;
        }

        public override void OnLowerStart(InputValue value)
        {
            crouch = true;
        }

        public override void OnJump(InputValue value)
        {
            jump = true;
        }

        public override void OnSprintFinish(InputValue value)
        {
            sprint = false;
        }

        public override void OnSprintStart(InputValue value)
        {
            sprint = true;
        }

        public virtual void OnRaiseStart(InputValue value) {}

        public virtual void OnRaiseFinish(InputValue value) {}
    }
}