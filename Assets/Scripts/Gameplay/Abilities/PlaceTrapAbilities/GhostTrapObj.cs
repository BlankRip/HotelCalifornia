using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities {
    public class GhostTrapObj : MonoBehaviour
    {
        [SerializeField] Transform trapProjection;
        [SerializeField] Transform nutrelizerProjection;
        [SerializeField] Transform trap;
        [SerializeField] Transform nutrelizer;

        private void Start() {
            trapProjection.gameObject.SetActive(true);
            nutrelizerProjection.gameObject.SetActive(true);
        }

        public void MoveTrapTo(Vector3 position) {
            trapProjection.position = position;
        }

        public void MoveNutralizerTo(Vector3 postion, Vector3 surfaceNormal) {
            nutrelizerProjection.position = postion;
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
    }
}