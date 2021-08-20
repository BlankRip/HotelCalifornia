using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RVD
{
    public class GhostController : MovementController
    {
        public void Update()
        {
            Movement();

            if (jump)
                controller.Move(transform.up * Time.deltaTime * speed);
            if (crouch)
                controller.Move(-transform.up * Time.deltaTime * speed);

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

        public override void OnRaiseFinish(InputValue value)
        {
            jump = false;
        }

        public override void OnRaiseStart(InputValue value)
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
    }
}