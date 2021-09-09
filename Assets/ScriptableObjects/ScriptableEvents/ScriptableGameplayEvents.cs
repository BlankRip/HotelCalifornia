using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Events
{
    [CreateAssetMenu()]
    public class ScriptableGameplayEvents : ScriptableObject
    {
        public UnityEvent onLeft;
        public UnityEvent onRight;
        public UnityEvent onUp;
        public UnityEvent onDown;
        public UnityEvent onClick;
        public UnityEvent onSpace;
        public UnityEvent onControl;
        public UnityEvent onShift;
        public UnityEvent onEscape;
    }
}