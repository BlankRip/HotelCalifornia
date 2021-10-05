using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public interface IInterfear
    {
        bool CanInterfear();
        void Interfear();
    }
}