using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public interface IMazeEntryRoom
    {
        List<Transform> GetPlayerEntryPoints();
        Transform GetExitPoint();
        void PieceCollected();
    }

    public interface IMazeManager {
        void SetStaticObjState(bool active);
        void SetUpMaze(GameObject exitTp);
        List<Transform> GetPlayerEntryPoints(int numberOfEntryPoints);
        void SpawnPieces(int piecesToCollect);
    }
}