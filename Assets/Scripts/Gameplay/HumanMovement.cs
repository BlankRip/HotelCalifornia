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

        private float crouchY = -1;
        private Transform playerModel;
        private bool resetlocalPos;

        private void Start() {
            if(cc == null) {
                cc = GetComponent<CharacterController>();
                if(cc == null)
                    cc = GetComponentInChildren<CharacterController>();
            }
            
            initialHight = cc.height;
            crouchHight = initialHight * (1 - crouchHightReductionMultipiler);
            
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
                if(crouchY == -1) {
                    PlayerModelFinder finder = GetComponentInChildren<PlayerModelFinder>();
                    if(finder != null) {
                        playerModel = finder.transform;
                        crouchY = playerModel.position.y;
                    }
                }
                resetlocalPos = true;
                
                float difference = Mathf.Abs(cc.height - crouchHight);
                if(difference > 0.005f) {
                    cc.height = Mathf.Lerp(cc.height, crouchHight, Time.deltaTime * 10);
                }
                playerModel.position = new Vector3(playerModel.position.x ,crouchY, playerModel.position.z);
                return true;
            } else {
                float difference = Mathf.Abs(cc.height - initialHight);
                if(difference > 0.005f) {
                    cc.height = Mathf.Lerp(cc.height, initialHight, Time.deltaTime * 10);
                    playerModel.position = new Vector3(playerModel.position.x ,crouchY, playerModel.position.z);
                    return true;
                }
            }
            
            if(resetlocalPos) {
                playerModel.localPosition = Vector3.zero;
                resetlocalPos = false;
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