using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public interface IMixer
    {
        public void AddPotion(IPortion type, GameObject gameObject);
        public void Mix();
    }
}