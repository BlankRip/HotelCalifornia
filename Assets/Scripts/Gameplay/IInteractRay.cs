using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IInteractRay
    {
        bool CanInteract();
        void Interact();
    }
}