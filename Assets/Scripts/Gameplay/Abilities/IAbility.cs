using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public interface IAbility
    {
        bool CanUse();
        void UseAbility();
        void Destroy();
    }
}