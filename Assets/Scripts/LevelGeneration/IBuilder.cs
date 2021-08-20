using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public interface IBuilder
    {
        void StartBuilder();
        bool GetBuilderStatus();
    }
}
