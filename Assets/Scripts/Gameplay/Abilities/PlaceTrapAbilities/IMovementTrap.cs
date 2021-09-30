using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public interface IMovementTrap
    {
        void MoveTrapTo(Vector3 position);
        void MoveNutralizerTo(Vector3 postion, Vector3 surfaceNormal);
        void SetTrap();
        GameObject GetGameObject();
    }
}