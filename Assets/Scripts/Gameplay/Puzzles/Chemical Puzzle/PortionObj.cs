using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class PortionObj : MonoBehaviour, IPortion
    {
        [SerializeField] ScriptablePortionMatDataBase matDataBase;
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] List<PortionType> baseSpawnableTypes;
        [SerializeField] GameObject liquidMat;
        private PortionType myType;
        private bool typeSet;

        private void Start() {
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
    }
}