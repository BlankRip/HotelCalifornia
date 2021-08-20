using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public interface ILevelSeed
    {
        void Initilize();
        void GenerateSeed();
    }
}
