using System.Collections;
using System.Collections.Generic;

namespace Knotgames.MazeGen {
    [System.Flags]
    public enum WallState {
        Left = 1, /*0001*/ Right = 2, /*0010*/
        Up = 4, /*0100*/ Down = 8, /*1000*/
        Visited = 32 // 10 0000
    }

    public class Neighbour {
        public Vector2Int position;
        public WallState sharedWall;

        public Neighbour(Vector2Int pos, WallState state) {
            position = pos;
            sharedWall = state;
        }
    }

    public class Vector2Int {
        public int x;
        public int y;

        public Vector2Int(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
}
