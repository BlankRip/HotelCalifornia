using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class DummyAbility : MonoBehaviour, IAbility
    {
        public bool CanUse() {
            return false;
        }

        public void Destroy() {
            Destroy(this);
        }

        public void UseAbility() {
            Debug.Log("Should never go here as this is dubmmy ability");
        }
    }
}