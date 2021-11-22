using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public class RadioPuzzleRoom : MonoBehaviour, IRadioPuzzleRoom, IPairPuzzleSetup
    {
        [SerializeField] Transform radioPuzzle;
        [SerializeField] List<Transform> radioSpots;
        private IRadioTuner puzzle;

        public void Link(GameObject obj, bool initiator)
        {
            puzzle = radioPuzzle.GetComponent<IRadioTuner>();
            puzzle.SetUp(obj.GetComponent<IRadioSolutionRoom>());
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }

        public void SetSolution(List<string> solution)
        {
            if (puzzle != null)
                puzzle.SetSolution(solution);
        }

        private void Awake()
        {
            int randSpot = KnotRandom.theRand.Next(0, radioSpots.Count);
            radioPuzzle.position = radioSpots[randSpot].position;
            radioPuzzle.rotation = radioSpots[randSpot].rotation;
        }
    }
}