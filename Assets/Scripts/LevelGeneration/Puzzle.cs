using UnityEngine;

namespace Knotgames.LevelGen {
    [System.Serializable]
    public class Puzzle: IPuzzle
    {
        [SerializeField] PuzzleType puzzleType;
        public Material mat;

        public void ActivatePuzzle(Renderer renderer) {
            renderer.material = mat;
        }

        public PuzzleType GetPuzzleType() {
            return puzzleType;
        }
    }
}
