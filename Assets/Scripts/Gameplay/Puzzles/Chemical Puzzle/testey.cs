using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public class testey : MonoBehaviour, IInteractable
    {
        Transform attachPos;
        bool held = false;
        Rigidbody rb;

        private void Start()
        {
            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            rb = GetComponent<Rigidbody>();
        }

        public void HideInteractInstruction()
        {
            Debug.LogError("HIDE ME BITCH!");
        }

        public void ShowInteractInstruction()
        {
            Debug.LogError("SHOW YOURSELF!");
        }

        public void Interact()
        {
            if (!held)
                Pick();
            else
                Drop();
        }

        public void Drop()
        {
            Debug.LogError("DROPPED");
            held = false;
            transform.SetParent(null);
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        public void Pick()
        {
            Debug.LogError("PICKED");
            held = true;
            transform.SetParent(attachPos);
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}