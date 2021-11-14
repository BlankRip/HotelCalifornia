using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class InterfearTrigger : MonoBehaviour, IAbility
    {
        private IInterfearRay myRay;
        private IAbilityUi myUi;
        private int usesLeft = 1;

        private void Start() {
            myRay = GetComponent<IInterfearRay>();
            myUi = GameObject.FindGameObjectWithTag("PrimaryUi").GetComponent<IAbilityUi>();
            myUi.UpdateObjectData(usesLeft);
        }

        public bool CanUse() {
            if(usesLeft != 0 && myRay.ObjectAvailable()) {
                if(myRay.GetInterfear().CanInterfear())
                    return true;
            }
            InstructionText.instance.ShowHideInstruction("Could not use Ability", 3);
            return false;
        }

        public void UseAbility() {
            usesLeft--;
            myRay.GetInterfear().Interfear();
            myUi.UpdateObjectData(usesLeft);
        }

        public void Destroy() {
            Destroy(this);
        }
    }
}