using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Knotgames.UI
{
    [CreateAssetMenu()]
    public class ScriptableUINavigate : ScriptableObject
    {
        public InputAction leftInputAction;
        public InputAction rightInputAction;
        public InputAction upInputAction;
        public InputAction downInputAction;
        public InputAction clickInputAction;
        public UnityEvent onLeft;
        public UnityEvent onRight;
        public UnityEvent onUp;
        public UnityEvent onDown;
        public UnityEvent onClick;
        private void OnEnable() {
            leftInputAction.performed += OnLeft;
            rightInputAction.performed += OnRight;
            upInputAction.performed += OnUp;
            downInputAction.performed += OnDown;
            clickInputAction.performed += OnClick;
        }
        public void OnLeft(InputAction.CallbackContext obj)
        {
            onLeft.Invoke();
        }
        public void OnRight(InputAction.CallbackContext obj)
        {
            onRight.Invoke();
        }
        public void OnUp(InputAction.CallbackContext obj)
        {
            onUp.Invoke();
        }
        public void OnDown(InputAction.CallbackContext obj)
        {
            onDown.Invoke();
        }
        public void OnClick(InputAction.CallbackContext obj)
        {
            onClick.Invoke();
        }
    }
}