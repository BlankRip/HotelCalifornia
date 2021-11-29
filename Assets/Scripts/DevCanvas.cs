using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI {
    public class DevCanvas : MonoBehaviour
    {
        private static DevCanvas instance;
        private void Awake() {
            if(instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}