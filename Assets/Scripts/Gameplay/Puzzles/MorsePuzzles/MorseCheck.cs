using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseCheck : MonoBehaviour, IInteractable
    {
        [SerializeField] MorsePuzzle linkedPuzzle;

        public void Interact() 
        {
            if(linkedPuzzle.CheckSolution())
                linkedPuzzle.Solved();
        }

        public void HideInteractInstruction() {}

        public void ShowInteractInstruction() {}
    }
}