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
        RepObj myObj;
        IReplicatePuzzle myPuzzle;
        [SerializeField] Transform attachPos;

        private void Awake()
        {
            myPuzzle = GetComponentInParent<IReplicatePuzzle>();
        }

        public string GetValue()
        {
            return myObj.name;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("RepObj"))
            {
                myObj = other.gameObject.GetComponent<RepObj>();
                myObj.Object.transform.SetPositionAndRotation(attachPos.position, attachPos.rotation); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("RepObj"))
            {
                myObj = null;
            }
        }

        public void Kill()
        {
            Destroy(myObj.Object);
        }
    }
}