using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IRayCaster
    {
        RaycastHit CastRay(LayerMask mask, float rayLength);
    }
}