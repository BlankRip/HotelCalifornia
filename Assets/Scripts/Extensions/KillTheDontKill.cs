using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Extensions
{
    public class KillTheDontKill : MonoBehaviour
    {
        public KillRef referer;
        public List<GameObject> toKill;
        private bool canDestroy = false;

        private void Awake()
        {
            if (referer.reference == null)
            {
                referer.reference = this;
                canDestroy = false;
            }
            else if (referer.reference != this)
            {
                canDestroy = true;
                foreach (GameObject go in toKill)
                    Destroy(go);
            }
        }

        private void OnDestroy()
        {
            if (!canDestroy)
                referer.reference = null;
        }
    }
}