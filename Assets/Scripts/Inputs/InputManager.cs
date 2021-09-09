using System.Collections;
using System.Collections.Generic;
using Knotgames.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Knotgames.Inputs
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        [SerializeField] ScriptableUIEvents scriptableUIEvents;
        [SerializeField] ScriptableInputManager scriptableInputManager;
        [SerializeField] PlayerInput playerInput;
        InputControls inputControls;
        InputAction movement;
        private static InputManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                scriptableInputManager.inputManager = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);

            inputControls = new InputControls();
            movement = inputControls.Gameplay.Move;
            movement.Enable();
        }

        public void OnLeft(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onLeft.Invoke();
        }
        public void OnRight(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onRight.Invoke();
        }
        public void OnUp(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onUp.Invoke();
        }
        public void OnDown(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onDown.Invoke();
        }
        public void OnEnter(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onEnter.Invoke();
        }
        public void OnEscape(InputAction.CallbackContext obj)
        {
            scriptableUIEvents.onEscape.Invoke();
        }

        public void SwapToUI()
        {
            playerInput.SwitchCurrentActionMap("UI");
        }

        public void SwapToGameplay()
        {
            playerInput.SwitchCurrentActionMap("Gameplay");
        }
    }
}