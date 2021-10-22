using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Gameplay {
    public class HumanMovement : MonoBehaviour, IPlayerMovement, IMoveAdjustment, IHumanMoveAdjustment
    {
        [SerializeField] CharacterController cc;
        [SerializeField] float movementSpeed = 10;
        [SerializeField] float gravity = -19.2f;
        [SerializeField] float jumpHight = 5;
        [SerializeField] [Range(0, 1)] float crouchSpeedMultiplier = 0.5f;
        private Vector3 velocity;
        private bool grounded;
        private bool inJump;
        private UnityEvent teleportEvent;

        private void Start() {
            if(cc == null) {
                cc = GetComponent<CharacterController>();
                if(cc == null)
                    cc = GetComponentInChildren<CharacterController>();
            }
            teleportEvent = new UnityEvent();
            
            if(gravity > 0)
                gravity *= -1;
        }

        private void Update() {
            grounded = cc.isGrounded;
        }

        public void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive)
        {
            float speed = movementSpeed;
            // if(moveYNegetive)
            //     speed *= crouchSpeedMultiplier;

            if (grounded && velocity.y < 0)
                velocity.y = -2;
            Vector3 moveDir = ((transform.forward * verticalInput) + (transform.right * horizontalInput)).normalized;
            Vector3 move = moveDir * speed * Time.deltaTime;
            cc.Move(move);

            if (moveYPositive) {
                if(inJump) {
                    moveYPositive = false;
                    inJump = false;
                }else if(grounded) {
                    velocity.y = Mathf.Sqrt(jumpHight * -2 * gravity);
                    inJump = true;
                } else
                    moveYPositive = false;
            }
            velocity.y += gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
        }

        public void SetOnNextTpEvent(UnityAction call) {
            teleportEvent.AddListener(call);
        }

        public void MoveToPosition(Vector3 position) {
            cc.enabled = false;
            transform.position = position;
            cc.enabled = true;
            teleportEvent.Invoke();
            teleportEvent.RemoveAllListeners();
        }

        public void InvokeTpEvent() {
            teleportEvent.Invoke();
            teleportEvent.RemoveAllListeners();
        }
    }
}