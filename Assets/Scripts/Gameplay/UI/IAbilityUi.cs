using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.UI {
    public interface IAbilityUi
    {
        void UpdateObjectData(int usesLeft, Sprite image);
        void UpdateObjectData(int usesLeft);
        GameObject GetGameObject();
    }
}