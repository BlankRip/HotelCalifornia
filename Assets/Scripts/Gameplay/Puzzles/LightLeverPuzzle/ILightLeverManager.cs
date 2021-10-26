using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public interface ILightLeverManager
    {
        int GetLightsNeeded();
        void SubstractNeedLights(int value);
        LightColour GetAvailableLeverColor();
        LightColour GetAvailableLightColor(ILight lightObj);
        List<ILight> GetLightsOfClour(LightColour colour);
        List<int> GetSolution();
        string GetColorHelper();
        List<LightColour> GetAllAvailableColors();
    }
}