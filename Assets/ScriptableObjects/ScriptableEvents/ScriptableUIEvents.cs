using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Knotgames.Events
{
    [CreateAssetMenu()]
    public class ScriptableUIEvents : ScriptableObject
    {
        public UnityEvent onLeft;
        public UnityEvent onRight;
        public UnityEvent onUp;
        public UnityEvent onDown;
        public UnityEvent onEnter;
        public UnityEvent onEscape;
    }
}