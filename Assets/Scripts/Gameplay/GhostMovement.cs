using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class GhostMovement : MonoBehaviour, IPlayerMovement, IGhostMoveAdjustment, IMoveAdjustment
    {
        [SerializeField] CharacterController cc;
        [SerializeField] float movementSpeed = 10;
        [SerializeField] float levitateSpeed = 5;
        private Vector3 previousPos;

        private void Start() {
            if(cc == null)
                cc = GetComponent<CharacterController>();
        }

        public void Move(float horizontalInput, float verticalInput, ref bool moveYPositive, ref bool moveYNegetive)
        {
            previousPos = this.transform.position;
            float levitateInput = 0;
            float levitationSpeed = levitateSpeed;
            if(moveYPositive || moveYNegetive) {
                levitateInput = 1;
                if(moveYNegetive)
                    levitationSpeed *= -1;
            }
            
            Vector3 moveDir = ((transform.forward * verticalInput) + (Vector3.up * levitateInput) + (transform.right * horizontalInput)).normalized;
            Vector3 move = moveDir;
            move.x = move.x * movementSpeed * Time.deltaTime;
            move.y = move.y * levitationSpeed * Time.deltaTime;
            move.z = move.z * movementSpeed * Time.deltaTime;
            cc.Move(move);
        }

        public float GetSpeed() {
            return movementSpeed;
        }

        public void AdjustSpeed(float speedMultiplier) {
            movementSpeed *= speedMultiplier;
        }

        public void SetSpeed(float speed) {
            movementSpeed = speed;
        }

        public void KnockBack() {
            Vector3 dir = (previousPos - this.transform.position).normalized;
            Vector3 moveTarget = dir * 4;
            cc.Move(moveTarget);
        }

        public void MoveToPosition(Vector3 position) {
            cc.enabled = false;
            transform.position = position;
            cc.enabled = true;
        }
    }
}