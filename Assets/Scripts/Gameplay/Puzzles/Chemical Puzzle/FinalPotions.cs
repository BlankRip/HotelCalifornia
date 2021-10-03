using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    [System.Serializable]
    public class FinalPotions
    {
        public PortionType part1, part2;

        public bool CompareParts(PortionType one, PortionType two)
        {
            Debug.LogError($"<color=yellow> Solution is {part1}, {part2} || Input is {one}, {two}</color>");
            if (one == part1 || one == part2)
                if (two == part1 || two == part2)
                    return true;

            return false;
        }
    }
}