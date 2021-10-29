using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapReset : MonoBehaviour, IInteractable
    {
        [SerializeField] MapSolution solution;

        public void Interact()
        {
            solution.ResetConnections();
        }

        public void HideInteractInstruction() { }

        public void ShowInteractInstruction() { }
    }
}