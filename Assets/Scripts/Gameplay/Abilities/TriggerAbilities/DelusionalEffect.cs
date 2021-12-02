using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class DelusionalEffect : MonoBehaviour, IAbilityEffect

    {
        [SerializeField] GameplayEventCollection eventCollection;

        public void ApplyEffect() {
            Debug.LogError("Start Delusions here");
            PlayerVissiionCorrupt.instance.SetEffectState(State.active);
            eventCollection.twistVision.Invoke();
        }

        public void ResetEffect() {
            Debug.LogError("Stope seeing Delusions");
            PlayerVissiionCorrupt.instance.SetEffectState(State.inactive);
            eventCollection.fixVision.Invoke();
        }
    }
}