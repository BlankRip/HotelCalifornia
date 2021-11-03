using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public interface IPuzzleActivator 
    {
        PuzzleType GetConnectableType();
        PuzzleType GetPuzzleType();
        void ActivatePuzzle(Renderer renderer);
        void Link(GameObject obj, bool intiator);
        GameObject GetActivatedObject();
    }

    public interface IPairPuzzleSetup 
    {
        void Link(GameObject obj, bool initiator);
    }
}
