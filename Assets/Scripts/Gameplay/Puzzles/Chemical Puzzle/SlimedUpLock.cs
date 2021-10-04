using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class SlimedUpLock : MonoBehaviour, IChemLock
    {
        [SerializeField] GameObject slimeObj;

        private PortionType desolverType;

        public void SetFinalPortionType(PortionType portionType) {
            desolverType = portionType;
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Potion")) {
                IPortion portion = other.GetComponent<IPortion>();
                if(portion.GetPortionType() == desolverType) {
                    slimeObj.SetActive(false);
                    Destroy(portion.GetGameObject());
                }
            }
        }
    }
}