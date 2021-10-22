using System.Collections;
using System.Collections.Generic;

namespace Knotgames.MazeGen {
    public static class MazeGenerator
    {   
        public static WallState[,] Generate(uint width, uint hight, int seed) {
            WallState[,] maze = new WallState[width, hight];
            WallState initialState = WallState.Left | WallState.Right | WallState.Up | WallState.Down;
            for (int i = 0; i < width; i++) {
                for (int j = 0; j < hight; j++)
                    maze[i, j] = initialState;
            }

            ApplyRecursiveBacktracker(ref maze, (int)width, (int)hight, seed);
            return maze;
        }

        private static void ApplyRecursiveBacktracker(ref WallState[,] maze, int width, int hight, int seed) {
            Stack<Vector2Int> needChecking = new Stack<Vector2Int>();
            System.Random rng = new System.Random(seed);
            Vector2Int pos = new Vector2Int(rng.Next(0, width), rng.Next(0, hight));
            maze[pos.x, pos.y] |= WallState.Visited;
            needChecking.Push(pos);

            Vector2Int current;
            List<Neighbour> neighbours = new List<Neighbour>();
            Neighbour neighbour = null;
            while(needChecking.Count > 0) {
                current = needChecking.Pop();
                neighbours = GetUnVisitedNeighoburs(current, maze, width, hight);
                if(neighbours.Count > 0) {
                    needChecking.Push(current);
                    neighbour = neighbours[rng.Next(0, neighbours.Count)];

                    maze[current.x, current.y] &= ~neighbour.sharedWall;
                    maze[neighbour.position.x, neighbour.position.y] &= ~GetOppositWall(neighbour.sharedWall);

                    maze[neighbour.position.x, neighbour.position.y] |= WallState.Visited;

                    needChecking.Push(neighbour.position);
                }
            }
        }


        private static List<Neighbour> GetUnVisitedNeighoburs(Vector2Int pos, WallState[,] maze, int width, int hight) {
            List<Neighbour> neighbours = new List<Neighbour>();
            if(pos.x > 0) { //left
                if(!maze[pos.x - 1, pos.y].HasFlag(WallState.Visited))
                    neighbours.Add(new Neighbour(new Vector2Int(pos.x - 1, pos.y), WallState.Left));
            }
            if(pos.y > 0) { //down
                if(!maze[pos.x, pos.y - 1].HasFlag(WallState.Visited))
                    neighbours.Add(new Neighbour(new Vector2Int(pos.x, pos.y - 1), WallState.Down));
            }
            if(pos.y < hight - 1) { //up
                if(!maze[pos.x, pos.y + 1].HasFlag(WallState.Visited))
                    neighbours.Add(new Neighbour(new Vector2Int(pos.x, pos.y + 1), WallState.Up));
            }
            if(pos.x < width - 1) { //right
                if(!maze[pos.x + 1, pos.y].HasFlag(WallState.Visited))
                    neighbours.Add(new Neighbour(new Vector2Int(pos.x + 1, pos.y), WallState.Right));
            }
            return neighbours;
        }

        private static WallState GetOppositWall(WallState state) {
            switch (state)
            {
                case WallState.Left:
                    return WallState.Right;
                case WallState.Right:
                    return WallState.Left;
                case WallState.Up:
                    return WallState.Down;
                case WallState.Down:
                    return WallState.Up;
                default:
                    return WallState.Left;
            }
        }
    }
}