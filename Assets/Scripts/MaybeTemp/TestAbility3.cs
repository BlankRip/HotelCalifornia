using System.Collections;
using System.Collections.Generic;
using Knotgames.Gameplay.UI;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TestAbility3 : MonoBehaviour, IAbility
    {
        private int usesLeft = 2;
        private IAbilityUi myUi;

        private void Start() {
            myUi = GameObject.FindGameObjectWithTag("UltimateUi").GetComponent<IAbilityUi>();
        }
        
        public bool CanUse() {
            if(usesLeft > 0) {
                //^ Do more check stuff here
                return true;
            } else {
                return false;
            }
        }

        public void UseAbility() {
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
            Debug.Log("The test ability was used");
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}