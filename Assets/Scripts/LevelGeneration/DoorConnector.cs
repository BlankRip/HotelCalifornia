using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay;

namespace Knotgames.LevelGen {
    public class DoorConnector : MonoBehaviour
    {
        [SerializeField] GameplayEventCollection gameplayEvents;

        private void Start() {
            gameplayEvents.gameStart.AddListener(ActivateDoorOpener);
        }

        private void OnDestroy() {
            gameplayEvents.gameStart.RemoveListener(ActivateDoorOpener);
        }

        private void ActivateDoorOpener() {
            GetComponent<Collider>().enabled = true;
        }
    }
}