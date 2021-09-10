using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerControler
    {
        void SetAbilities(List<IAbility> abilities);
    }
}