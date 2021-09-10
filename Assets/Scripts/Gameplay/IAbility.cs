using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IAbility
    {
        bool CanUse();
        void UseAbility();
    }
}