using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class SinglePuzzleRoom : MonoBehaviour, ISingelPuzzleRoom
    {
        public List<PuzzleActivator> roomPuzzles;
        [SerializeField] private Renderer renderer;

        public PuzzleType GetAndActivePuzzle(ref List<PuzzleType> exclusionTypes) {
            int rand = KnotRandom.theRand.Next(0, roomPuzzles.Count);
            if(!exclusionTypes.Contains(roomPuzzles[rand].GetPuzzleType())) {
                roomPuzzles[rand].ActivatePuzzle(renderer);
                exclusionTypes.Add(roomPuzzles[rand].GetPuzzleType());
                Debug.Log($"<color=pink> {roomPuzzles[rand].GetPuzzleType()} </color>", this.gameObject);
                return roomPuzzles[rand].GetPuzzleType();
            } else {
                return PuzzleType.Nada;
            }
        }

        public void SelfKill() {
            Destroy(this.gameObject);
        }
    }
}
