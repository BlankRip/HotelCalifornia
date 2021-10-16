using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public class RadioSolutionRoom : MonoBehaviour, IRadioSolutionRoom, IPairPuzzleSetup
    {
        [SerializeField] GameObject radioSolutionObj;
        [SerializeField] List<Transform> radioSpots;
        private IRadioSolution radioSolution;
        [SerializeField] List<string> currentSolution;
        private IRadioPuzzleRoom puzzleRoom;

        private void Start()
        {
            radioSolution = GameObject.Instantiate(radioSolutionObj).GetComponent<IRadioSolution>();
            radioSolution.SetupRadio();
            SetUpSolution();
        }

        private void SetUpSolution()
        {
            int rand = Random.Range(0, radioSpots.Count);
            currentSolution = radioSolution.BuildNewSolution(radioSpots[rand]);
            if (puzzleRoom != null)
                puzzleRoom.SetSolution(currentSolution);
        }

        public void Solved()
        {
            Destroy(this);
        }

        public void Link(GameObject obj, bool initiator)
        {
            puzzleRoom = obj.GetComponent<IRadioPuzzleRoom>();
            puzzleRoom.SetSolution(currentSolution);
            if (initiator)
                obj.GetComponent<IPairPuzzleSetup>().Link(this.gameObject, false);
        }
    }
}