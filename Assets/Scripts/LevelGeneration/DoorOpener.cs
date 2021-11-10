using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen { 
    public class DoorOpener : MonoBehaviour
    {
        [SerializeField] GameObject closeDoor;
        [SerializeField] GameObject openDoor;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("DoorConnect")) {
                openDoor.SetActive(true);
                closeDoor.SetActive(false);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}