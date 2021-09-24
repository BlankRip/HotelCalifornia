using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] Animator animator;
        [SerializeField] string horizontalSpeed = "hSpeed";
        [SerializeField] string verticalSpeed = "vSpeed";
        [SerializeField] string runningBool = "run";
        [SerializeField] string positiveYBool = "jump";
        [SerializeField] string negetiveYBool = "crouch";
        private bool running;

        private void Awake() {
            if(animator == null)
                animator = GetComponent<Animator>();
        }

        public void Animate(float horizontalInput, float verticalInput, bool yPositive, bool yNegetive) {
            animator.SetBool(positiveYBool, yPositive);
            animator.SetBool(negetiveYBool, yNegetive);

            if((horizontalInput != 0 || verticalInput != 0) && !yPositive) {
                if(!running) {
                    running = true;
                    animator.SetBool(runningBool, running);
                }
            } else {
                if(running) {
                    running = false;
                    animator.SetBool(runningBool, running);
                }
            }

            // animator.SetFloat(horizontalSpeed, horizontalInput);
            // animator.SetFloat(verticalSpeed, verticalInput);
        }

        public bool GetBool(string id) {
            return animator.GetBool(id);
        }

        public void SetBool(string id, bool value) {
            animator.SetBool(id, value);
        }

        public void SetTrigger(string id) {
            animator.SetTrigger(id);
        }
    }
}