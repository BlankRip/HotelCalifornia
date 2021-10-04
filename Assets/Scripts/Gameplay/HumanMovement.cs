using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class HumanMovement : MonoBehaviour, IPlayerMovement, IMoveAdjustment
    {
        [SerializeField] CharacterController cc;
        [SerializeField] float movementSpeed = 10;
        [SerializeField] float gravity = -19.2f;
        [SerializeField] float jumpHight = 5;
        [SerializeField] [Range(0, 1)] float crouchSpeedMultiplier = 0.5f;
        [SerializeField] [Range(0, 1)] float crouchHightReductionMultipiler = 0.7f;
        private Vector3 velocity;
        private float initialHight;
        private float crouchHight;
        private bool grounded;
        private bool inJump;

        private void Start() {
            if(cc == null)
                cc = GetComponent<CharacterController>();
            
            initialHight = cc.height;
            crouchHight = initialHight * (1 - crouchHightReductionMultipiler);
            Debug.Log(crouchHight);
            
            if(gravity > 0)
                gravity *= -1;
        }
        private void Update() {
            grounded = cc.isGrounded;
        }

        public void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive)
        {
            if(CrouchUnderEffect(moveYNegetive)) {
                velocity.y += gravity * Time.deltaTime;
                cc.Move(velocity * Time.deltaTime);
                return;
            }

            float speed = movementSpeed;
            if(moveYNegetive)
                speed *= crouchSpeedMultiplier;

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

        private bool CrouchUnderEffect(bool crouching) {
            if(crouching && grounded) {
                float difference = Mathf.Abs(cc.height - crouchHight);
                if(difference > 0.005f) {
                    cc.height = Mathf.Lerp(cc.height, crouchHight, Time.deltaTime * 10);
                }
                return true;
            } else {
                float difference = Mathf.Abs(cc.height - initialHight);
                if(difference > 0.005f) {
                    cc.height = Mathf.Lerp(cc.height, initialHight, Time.deltaTime * 10);
                    return true;
                }
            }

            return false;
        }

        public void MoveToPosition(Vector3 position) {
            cc.enabled = false;
            transform.position = position;
            cc.enabled = true;
        }
    }
}