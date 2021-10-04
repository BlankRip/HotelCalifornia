using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public interface IChemicalPuzzle
    {
        List<PortionType> GetSpawnedTypes();
        List<MixerSolution> GetSolutions();
        void AddToSpawnedList(PortionType portionType);
    }
}