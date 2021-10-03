using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class ChemicalPuzzle : MonoBehaviour, IChemicalPuzzle
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] List<Transform> portionSpawnPoints;
        [SerializeField] GameObject portionObj;
        [SerializeField] int numberToSpawn = 5;

        private SpawnAtGivenPoints portionSpawner;
        private List<PortionType> spawnedTypes;
        private List<PortionType> availableResultants;
        private List<MixerSolution> solutions;


        private void Awake() {
            portionSpawner = new SpawnAtGivenPoints(portionSpawnPoints, portionObj, numberToSpawn, true);
            spawnedTypes = new List<PortionType>();
            availableResultants = new List<PortionType>();
            solutions = new List<MixerSolution>();
            chemRoom.manager = this;
        }

        private void Start() {
            portionSpawner.Spawn();
        }

        private void GeneratePuzzle() {
            CullResultantsList();
            List<PortionType> availableTypes = new List<PortionType>(spawnedTypes);

            while(availableTypes.Count > 1) {
                
            }
        }

        private void CullResultantsList() {
            //Debug.Log(Enum.GetValues(typeof(PortionType)).Length);
            for (int i = 0; i < Enum.GetValues(typeof(PortionType)).Length; i++)
                availableResultants.Add((PortionType)i);
            
            foreach(PortionType portionType in spawnedTypes)
                availableResultants.Remove(portionType);
        }

        public List<PortionType> GetSpawnedTypes() {
            return spawnedTypes;
        }

        public void AddToSpawnedList(PortionType portionType) {
            spawnedTypes.Add(portionType);
        }
    }

    public class MixerSolution 
    {
        public List<PortionType> mixTypes;
        public PortionType resultantType;

        public MixerSolution() {
            mixTypes = new List<PortionType>();
        }
    }
}