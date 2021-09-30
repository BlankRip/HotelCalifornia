using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public interface ITrapRayRequirements
    {
        ScriptableRayCaster GetRayCaster();
        LayerMask GetGroundLayers();
        LayerMask GetPlacableLayers();

        //TODO Remove as this is just for testing
        GameObject GetTrapObj();
    }
}