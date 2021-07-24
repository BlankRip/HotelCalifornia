using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Blank.LevelGen {
    public interface IBuilder
    {
        void StartBuilder();
        bool GetBuilderStatus();
        bool HasFailed();
    }
}
