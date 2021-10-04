using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public interface IPortion
    {
        PortionType GetPortionType();
        void SetPortionType(PortionType type);
        void Drop();
        GameObject GetGameObject();
        void SetMySlot(IMixerSlot mySlot);
    }

    public enum PortionType {
        Red, Green, Blue, Yellow,
        Brown, Orange, Grey, Purple,
        Cream, Black
    }
}