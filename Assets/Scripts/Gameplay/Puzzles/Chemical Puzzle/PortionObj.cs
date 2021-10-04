using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class PortionObj : MonoBehaviour, IPortion, IInteractable
    {
        [SerializeField] ScriptablePortionMatDataBase matDataBase;
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] List<PortionType> baseSpawnableTypes;
        [SerializeField] GameObject liquidMat;
        private PortionType myType;
        private bool typeSet;

        Transform attachPos;
        bool held = false;
        Rigidbody rb;
        private Vector3 restPos;
        private IMixerSlot mySlot;

        private void Start() {
            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            rb = GetComponent<Rigidbody>();

            CullSpawnableList();
            if(baseSpawnableTypes.Count > 0) {
                int rand = Random.Range(0, baseSpawnableTypes.Count);
                SetPortionType(baseSpawnableTypes[rand]);
            }
            restPos = transform.position;
        }

        private void CullSpawnableList() {
            List<PortionType> spawnedList = chemRoom.manager.GetSpawnedTypes();
            foreach (PortionType type in spawnedList) {
                if(baseSpawnableTypes.Contains(type))
                    baseSpawnableTypes.Remove(type);
            }
        }

        public PortionType GetPortionType() {
            return myType;
        }

        public void SetPortionType(PortionType type) {
            if(!typeSet) {
                myType = type;
                liquidMat.GetComponent<Renderer>().material = matDataBase.GetMaterial(myType);
                chemRoom.manager.AddToSpawnedList(type);
                typeSet = true;
            }
        }

        public void HideInteractInstruction() {}

        public void ShowInteractInstruction() {}

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
            if(mySlot != null) {
                if(mySlot.CanReturn()) {
                    mySlot.ReturingFromSlot();
                    mySlot = null;
                } else return;
            }
            restPos = transform.position;
            held = true;
            transform.SetParent(attachPos);
            transform.localPosition = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("GhostEdge"))
                transform.position = restPos;
        }

        public void SetMySlot(IMixerSlot mySlot) {
            this.mySlot = mySlot;
        }
    }
}