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

        private void Start() {
            attachPos = GameObject.FindGameObjectWithTag("AttachPos").transform;
            rb = GetComponent<Rigidbody>();

            CullSpawnableList();
            int rand = Random.Range(0, baseSpawnableTypes.Count);
            SetPortionType(baseSpawnableTypes[rand]);
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
                Drop(false);
        }

        public void Drop(bool isKinematic)
        {
            Debug.LogError("DROPPED");
            held = false;
            transform.SetParent(null);
            rb.isKinematic = isKinematic;
            rb.useGravity = !isKinematic;
        }

        public void Pick()
        {
            held = true;
            transform.SetParent(attachPos);
            rb.useGravity = false;
            rb.isKinematic = true;
            restPos = transform.position;
        }

        public GameObject GetGameObject() {
            return this.gameObject;
        }

        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.CompareTag("GhostEdge"))
                transform.position = restPos;
        }
    }
}