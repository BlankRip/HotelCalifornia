using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class TestingPairSetUp : MonoBehaviour
    {
        [SerializeField] PuzzleActivator A;
        [SerializeField] PuzzleActivator B;

        private void Start() {
            IPuzzleActivator aActivator = A;
            IPuzzleActivator bActivator = B;

            aActivator.ActivatePuzzle(null);
            bActivator.ActivatePuzzle(null);
            aActivator.Link(bActivator.GetActivatedObject(), true);
        }
    }
}