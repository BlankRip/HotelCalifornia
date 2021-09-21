using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerAnimator
    {
        void Animate(float horizontalInput, float verticalInput, bool yPositive, bool yNegetive);
        bool GetBool(string id);
        void SetBool(string id, bool value);
        void SetTrigger(string id);
    }
}