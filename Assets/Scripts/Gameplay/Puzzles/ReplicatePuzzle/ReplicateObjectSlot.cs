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
        public ReplicateObject myObj;
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
            return myObj.name;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RepObj"))
            {
                myObj = other.gameObject.GetComponent<ReplicateObject>();
                myObj.Drop();
                myObj.slotted = true;
                myObj.mySlot = this;
                myObj.transform.SetPositionAndRotation(attachPos.position, attachPos.rotation);
                myCollider.enabled = false;
            }
        }

        public void Kill()
        {
            Destroy(myObj);
        }
    }
}