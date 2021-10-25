using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public interface ILightLeverManager
    {
        int GetLightsNeeded();
        LightColour GetAvailableLeverColor();
        LightColour GetAvailableLightColor(ILight lightObj);
        List<ILight> GetLightsOfClour(LightColour colour);
    }
}