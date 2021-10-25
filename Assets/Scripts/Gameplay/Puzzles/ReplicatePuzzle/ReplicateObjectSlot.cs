using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;
using System;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateObjectSlot : MonoBehaviour, IReplicateSlot
    {
        public IReplicateObject myObj;
        IReplicatePuzzle myPuzzle;
        [SerializeField] Transform attachPos;
        [HideInInspector] public Collider myCollider;
        string slottedName;
        MeshRenderer meshRenderer;

        private void Awake()
        {
            myPuzzle = GetComponentInParent<IReplicatePuzzle>();
            myCollider = GetComponent<Collider>();
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
            if (!DevBoy.yes)
                NetUnityEvents.instance.slotHideStatus.AddListener(ReceiveData);
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
                if(myObj == null)
                {
                    other.transform.SetParent(transform);
                    myObj = other.gameObject.GetComponent<IReplicateObject>();
                    myObj.Drop(true, attachPos);
                    slottedName = myObj.GetName();
                    myObj.HandleSlotting(this, attachPos.position, attachPos.rotation);
                    myCollider.enabled = false;
                    myPuzzle.CheckSolution();
                }
            }
        }

        public void SetPuzzle(ReplicatePuzzle replicatePuzzle)
        {
            myPuzzle = replicatePuzzle;
        }

        public void Disable()
        {
            myObj.Disable();
            Destroy(this);
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

        public void ReceiveData(string received)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(received);
            if (extracted.eventName == "slotHideStatus")
            {
                Debug.LogError("IM RIGHT HERE!");
                meshRenderer.enabled = extracted.status;
            }
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.slotHideStatus.RemoveListener(ReceiveData);
        }

        private class ExtractionClass
        {
            public string eventName;
            public bool status;
        }
    }
}