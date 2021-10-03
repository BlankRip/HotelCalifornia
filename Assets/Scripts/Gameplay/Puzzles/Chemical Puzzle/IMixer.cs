using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public interface IMixer
    {
        public void AddPotion(PortionType type, GameObject gameObject);
        public void Mix();
    }
}