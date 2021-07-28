using System.Collections;
using System.Collections.Generic;
//using Knotgames.Alwin.Network;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RVD
{
    public interface IInputReader
    {
        void OnRaiseStart(InputValue value);
        void OnRaiseFinish(InputValue value);
        void OnMove(InputValue value);
        void OnMouseX(InputValue value);
        void OnMouseY(InputValue value);
        void OnSprintStart(InputValue value);
        void OnSprintFinish(InputValue value);
        void OnLowerStart(InputValue value);
        void OnLowerFinish(InputValue value);
    }
}