using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseCheck : MonoBehaviour, IInteractable
    {
        [SerializeField] MorseDevice linkedPuzzle;

        public void Interact() 
        {
            if(linkedPuzzle.CheckSolution())
                Solve();
        }

        void Solve()
        {
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            linkedPuzzle.Solved();
        }

        public void HideInteractInstruction() {}

        public void ShowInteractInstruction() {}
    }
}