using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateObjectSlot : MonoBehaviour, IReplicateSlot
    {
        [SerializeField] ReplicateObjectDatabase replicateObjectDatabase;
        public IReplicateObject myObj;
        IReplicatePuzzle myPuzzle;
        [SerializeField] Transform attachPos;
        [HideInInspector] public Collider myCollider;

        private void Awake()
        {
            myPuzzle = GetComponentInParent<IReplicatePuzzle>();
            myCollider = GetComponent<Collider>();
        }

        public string GetValue()
        {
            return myObj.GetName();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RepObj"))
            {
                myObj = other.gameObject.GetComponent<ReplicateObject>();
                myObj.Drop(true);
                myObj.HandleSlotting(this, attachPos.position, attachPos.rotation);
                myCollider.enabled = false;
            }
        }

        public void SetNull()
        {
            myObj = null;
        }

        public void SetCollider(bool value)
        {
            myCollider.enabled = value;
        }
    }
}