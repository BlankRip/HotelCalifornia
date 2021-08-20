using System.Collections;
using System.Collections.Generic;
using RVD;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour, IInputReader
{
    public virtual void OnEscape(InputValue value) {}
    public void OnLowerFinish(InputValue value) {}
    public void OnLowerStart(InputValue value) {}
    public void OnMouseX(InputValue value) {}
    public void OnMouseY(InputValue value) {}
    public void OnMove(InputValue value) {}
    public void OnRaiseFinish(InputValue value) {}
    public void OnRaiseStart(InputValue value) {}
    public void OnSprintFinish(InputValue value) {}
    public void OnSprintStart(InputValue value) {}
    public virtual void SetUIControls() {}
}