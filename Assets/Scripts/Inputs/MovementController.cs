using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RVD
{
    public class MovementController : MonoBehaviour, IInputReader
    {
        protected Vector2 moveInput, mouseInput;
        [SerializeField] protected CharacterController controller;
        [SerializeField] protected float speed = 11f;
        [SerializeField] protected float jumpHeight = 3.5f;
        protected bool jump = false, sprint = false, crouch = false;
        protected Vector3 verticalVelocity = Vector3.zero;
        [SerializeField] protected float sensitivityX = 8f;
        [SerializeField] protected float sensitivityY = 0.5f;
        protected float mouseX, mouseY;
        [SerializeField] protected Transform playerCam;
        [SerializeField] protected float xClamp = 85f;
        [SerializeField] protected float sprintMultiplier = 1.25f, crouchMultiplier = 0.75f;
        [SerializeField] protected float gravity = -30f;
        protected float xRotation = 0f;
        public bool useGravity = false;

        public void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        protected void Movement()
        {
            Vector3 horizontalVelocity = (transform.right * moveInput.x + transform.forward * moveInput.y) * speed;

            if (useGravity && !controller.isGrounded)
                verticalVelocity.y += gravity * Time.deltaTime;

            if (sprint)
            {
                controller.Move(horizontalVelocity * Time.deltaTime * sprintMultiplier);
                controller.Move(verticalVelocity * Time.deltaTime * sprintMultiplier);
            }
            else if (crouch)
            {
                controller.Move(horizontalVelocity * Time.deltaTime * crouchMultiplier);
                controller.Move(verticalVelocity * Time.deltaTime * crouchMultiplier);
            }
            else
            {
                controller.Move(horizontalVelocity * Time.deltaTime);
                controller.Move(verticalVelocity * Time.deltaTime);
            }
        }

        public virtual void OnLowerFinish(InputValue value) {}

        public virtual void OnLowerStart(InputValue value) {}

        public void OnMouseX(InputValue value)
        {
            mouseInput.x = value.Get<float>();
            mouseX = mouseInput.x * sensitivityX;
        }

        public void OnMouseY(InputValue value)
        {
            mouseInput.y = value.Get<float>();
            mouseY = mouseInput.y * sensitivityY;
        }

        public void OnMove(InputValue value)
        {
            Vector2 moveValue = value.Get<Vector2>();
            moveInput = new Vector2(moveValue.x, moveValue.y);
        }

        public virtual void OnRaiseFinish(InputValue value) {}

        public virtual void OnRaiseStart(InputValue value){}

        public virtual void OnSprintFinish(InputValue value) {}

        public virtual void OnSprintStart(InputValue value) {}

        public virtual void OnJump(InputValue value) {}

        public void OnEscape(InputValue value) {}

        public void SetUIControls() {}
    }
}