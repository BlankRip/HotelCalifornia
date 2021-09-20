using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class PlayerAnimator : MonoBehaviour, IPlayerAnimator
    {
        [SerializeField] Animator animator;
        [SerializeField] string horizontalSpeed = "hSpeed";
        [SerializeField] string verticalSpeed = "vSpeed";
        [SerializeField] string positiveYBool = "jump";
        [SerializeField] string negetiveYBool = "crouch";

        private void Awake() {
            if(animator == null)
                animator = GetComponent<Animator>();
        }

        public void Animate(float horizontalInput, float verticalInput, bool yPositive, bool yNegetive) {
            animator.SetBool(positiveYBool, yPositive);
            animator.SetBool(negetiveYBool, yNegetive);
            animator.SetFloat(horizontalSpeed, horizontalInput);
            animator.SetFloat(verticalSpeed, verticalInput);
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