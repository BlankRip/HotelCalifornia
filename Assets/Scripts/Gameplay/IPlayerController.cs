using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerController
    {
        void SetAbilities(List<IAbility> abilities);
        void SwapSecondary(IAbility ability);
        GameObject GetPlayerObject();
    }
}