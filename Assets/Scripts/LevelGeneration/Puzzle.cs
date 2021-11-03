using UnityEngine;
using System.Collections.Generic;

namespace Knotgames.LevelGen {
    [System.Serializable]
    public class PuzzleActivator: IPuzzleActivator
    {
        [SerializeField] PuzzleType puzzleType;
        [SerializeField] PuzzleType canConnectTo;
        [SerializeField] GameObject activationObject;
        public Material mat;

        public void ActivatePuzzle(Renderer renderer) {
            if(renderer != null)
                renderer.material = mat;
            activationObject.SetActive(true);
        }

        public void Link(GameObject obj, bool intiator) {
            activationObject.GetComponent<IPairPuzzleSetup>().Link(obj, intiator);
        }

        public PuzzleType GetPuzzleType() {
            return puzzleType;
        }

        public GameObject GetActivatedObject() {
            return activationObject;
        }

        public PuzzleType GetConnectableType() {
            return canConnectTo;
        }
    }
}
