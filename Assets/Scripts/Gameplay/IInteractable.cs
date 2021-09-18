using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IInteractable
    {
        void ShowInteractInstruction();
        void HideInteractInstruction();
        void Interact();
    }
}