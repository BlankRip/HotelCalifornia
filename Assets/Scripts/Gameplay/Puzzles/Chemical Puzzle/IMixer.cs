using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public interface IMixer
    {
        void AddPotion(IPortion type, IMixerSlot slot);
        void RemovePortion(IPortion type, IMixerSlot slot);
        void StartMix();
        bool IsMixing();
    }

    public interface IMixerSlot {
        void DestroyItemInSlot();
        bool CanReturn();
        void ReturingFromSlot();
    }
}