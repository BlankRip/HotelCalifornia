using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class DoorEntryWidthAdjuster : MonoBehaviour
    {
        private float defaultWidth = -1;
        private float widthThatWillFit = 0.5f;

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Human")) {
                if(defaultWidth == -1)
                    defaultWidth = other.GetComponent<CharacterController>().radius;
                other.GetComponent<CharacterController>().radius = widthThatWillFit;
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag("Human")) {
                other.GetComponent<CharacterController>().radius = defaultWidth;
            }
        }
    }
}