using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;

namespace Knotgames.Gameplay {
    public interface IPlayerController
    {
        void SetAbilities(List<IAbility> abilities);
        void SwapSecondary(IAbility ability);
        GameObject GetPlayerObject();
    }
}