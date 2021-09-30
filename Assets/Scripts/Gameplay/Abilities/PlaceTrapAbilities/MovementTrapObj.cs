using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class MovementTrapObj : MonoBehaviour, IMovementTrap
    {
        [SerializeField] Transform trapProjection;
        [SerializeField] Transform nutrelizerProjection;
        [SerializeField] Transform trap;
        [SerializeField] Transform nutrelizer;
        private float distance;
        private float maxDistance = 20;

        private void Start() {
            trapProjection.gameObject.SetActive(true);
            nutrelizerProjection.gameObject.SetActive(true);
            maxDistance = maxDistance * maxDistance;
        }

        public void MoveTrapTo(Vector3 position) {
            trapProjection.position = position;
        }

        public void MoveNutralizerTo(Vector3 postion, Vector3 surfaceNormal) {
            distance = (postion - trapProjection.position).sqrMagnitude;
            if(distance <= maxDistance) {
                nutrelizerProjection.position = postion;
                nutrelizerProjection.up = surfaceNormal;
            }
        }

        public void SetTrap() {
            trap.position = trapProjection.position;
            nutrelizer.position = nutrelizerProjection.position;
            nutrelizer.rotation = nutrelizerProjection.rotation;

            trapProjection.gameObject.SetActive(false);
            nutrelizerProjection.gameObject.SetActive(false);
            trap.gameObject.SetActive(true);
            nutrelizer.gameObject.SetActive(true);
        }

        public void DestroyTrap() {
            Destroy(this.gameObject);
        }
    }
}