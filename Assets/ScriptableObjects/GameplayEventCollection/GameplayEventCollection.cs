using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Gameplay {
    [CreateAssetMenu()]
    public class GameplayEventCollection : ScriptableObject
    {
        public UnityEvent twistVision;
        public UnityEvent fixVision;
        public UnityEvent frequencyMessup;
        public UnityEvent normalFrequency;
    }
}