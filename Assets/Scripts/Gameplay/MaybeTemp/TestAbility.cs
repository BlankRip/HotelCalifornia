using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TestAbility : MonoBehaviour, IAbility
    {
        public bool CanUse() {
            return true;
        }

        public void UseAbility() {
            Debug.Log("The test ability was used");
        }
    }
}