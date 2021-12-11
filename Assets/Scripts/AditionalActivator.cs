using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames {
    public class AditionalActivator : MonoBehaviour
    {
        [SerializeField] List<GameObject> toActivate;

        private void OnEnable() {
            foreach (GameObject go in toActivate)
                go.SetActive(true);
        }

        private void OnDisable() {
            foreach (GameObject go in toActivate)
                go.SetActive(false);
        }
    }
}