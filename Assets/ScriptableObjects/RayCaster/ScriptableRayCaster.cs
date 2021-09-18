using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    [CreateAssetMenu()]
    public class ScriptableRayCaster : ScriptableObject
    {
        public IRayCaster caster;
    }
}