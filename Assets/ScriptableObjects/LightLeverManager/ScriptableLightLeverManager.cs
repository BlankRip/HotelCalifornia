using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    [CreateAssetMenu()]
    public class ScriptableLightLeverManager : ScriptableObject
    {
        public ILightLeverManager manager;
        public void Initilize() {
            manager = new LightLeverManager();
        }
    }
}