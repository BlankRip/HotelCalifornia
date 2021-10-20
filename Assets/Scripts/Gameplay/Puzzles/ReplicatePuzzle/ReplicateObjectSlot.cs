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
        string slottedName;

        private void Awake()
        {
            myPuzzle = GetComponentInParent<IReplicatePuzzle>();
            myCollider = GetComponent<Collider>();
        }

        public string GetValue()
        {
            Debug.LogError($"THIS IS NAME : {slottedName}");
            return slottedName;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("RepObj"))
            {
                myObj = other.gameObject.GetComponent<IReplicateObject>();
                myObj.Drop(true);
                slottedName = myObj.GetName();
                myObj.HandleSlotting(this, attachPos.position, attachPos.rotation);
                myCollider.enabled = false;
                myPuzzle.CheckSolution();
            }
        }

        public void SetNull()
        {
            slottedName = "";
            myObj = null;
        }

        public void SetCollider(bool value)
        {
            myCollider.enabled = value;
        }
    }
}