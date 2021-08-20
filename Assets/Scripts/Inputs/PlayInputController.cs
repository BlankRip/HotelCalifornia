using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Knotgames.Inputs {
    public class PlayInputController : MonoBehaviour, IUi, IInputReader
    {
        private UnityEvent onEscape;
        [SerializeField] ScriptableUiInputs ui;

        private void Awake() {
            ui.uiInputs = this;
        }

        private void Start() {
            onEscape = new UnityEvent();
        }

        public void SetOnEscapeEvent(UnityAction call) {
            onEscape.RemoveAllListeners();
            onEscape.AddListener(call);
        }

        public void SwitchToGamePlayInputs() {
            throw new System.NotImplementedException();
        }

        public void SwitchToUiInputs() {
            throw new System.NotImplementedException();
        }

        public void OnRaiseStart(InputValue value) {
            throw new System.NotImplementedException();
        }

        public void OnRaiseFinish(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnMouseX(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnMouseY(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnSprintStart(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnSprintFinish(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnLowerStart(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnLowerFinish(InputValue value)
        {
            throw new System.NotImplementedException();
        }

        public void OnEscape(InputValue value)
        {
            onEscape.Invoke();
        }

        public void SetUIControls()
        {
            throw new System.NotImplementedException();
        }
    }
}
