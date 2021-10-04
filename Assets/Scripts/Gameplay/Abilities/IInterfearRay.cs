using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public interface IInterfearRay
    {
        IInterfear GetInterfear();
        bool ObjectAvailable();
    }
}