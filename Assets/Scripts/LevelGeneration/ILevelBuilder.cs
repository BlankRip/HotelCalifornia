using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public interface ILevelBuilder
    {
        void StartLevelGen(bool genSeed);
    }
}