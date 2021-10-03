using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class ChemicalPuzzle : MonoBehaviour, IChemicalPuzzle
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] List<Transform> portionSpawnPoints;
        [SerializeField] GameObject portionObj;
        [SerializeField] int numberToSpawn = 5;

        private SpawnAtGivenPoints portionSpawner;
        private List<PortionType> spawnedTypes;


        private void Awake() {
            portionSpawner = new SpawnAtGivenPoints(portionSpawnPoints, portionObj, numberToSpawn, true);
            spawnedTypes = new List<PortionType>();
            chemRoom.manager = this;
        }

        private void Start() {
            portionSpawner.Spawn();
        }

        public List<PortionType> GetSpawnedTypes() {
            return spawnedTypes;
        }

        public void AddToSpawnedList(PortionType portionType) {
            spawnedTypes.Add(portionType);
        }
    }
}