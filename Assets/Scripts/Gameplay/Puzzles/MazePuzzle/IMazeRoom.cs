using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public interface IMazeEntryRoom
    {
        List<Transform> GetPlayerEntryPoints();
        Transform GetExitPoint();
    }

    public interface IMazeRoom {
        void SpawnMaze();
        List<Transform> GetPlayerEntryPoints();
        Transform GetExitPoint();
    }
}