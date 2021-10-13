using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class SinglePuzzleRoom : MonoBehaviour, ISingelPuzzleRoom
    {
        public List<PuzzleActivator> roomPuzzles;
        [SerializeField] private Renderer renderer;

        public PuzzleType GetAndActivePuzzle(ref List<PuzzleType> exclusionTypes) {
            int rand = Random.Range(0, roomPuzzles.Count);
            if(!exclusionTypes.Contains(roomPuzzles[rand].GetPuzzleType())) {
                roomPuzzles[rand].ActivatePuzzle(renderer);
                exclusionTypes.Add(roomPuzzles[rand].GetPuzzleType());
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
