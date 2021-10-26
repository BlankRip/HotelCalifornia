using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public interface ILightLeverManager
    {
        int GetLightsNeeded();
        void SubstractNeededLights(int value);
        LightColor GetAvailableLeverColor();
        LightColor GetAvailableLightColor(ILight lightObj);
        List<ILight> GetLightsOfColor(LightColor colour);
        List<int> GetSolution();
        string GetColorHelper();
        List<LightColor> GetAllAvailableColors();
    }
}