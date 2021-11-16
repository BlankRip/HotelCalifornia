using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class ChemicalPuzzle : MonoBehaviour, IChemicalPuzzle
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] List<Transform> portionSpawnPoints;
        [SerializeField] GameObject portionObj;
        [SerializeField] int numberToSpawn = 5;
        [SerializeField] List<SolutionPad> solutionPads;
        [SerializeField] GameObject lockObj;
        private IChemLock chemLock;

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
            chemLock = lockObj.GetComponent<IChemLock>();
        }

        private void Start() {
            StartCoroutine(SetUp());
        }

        private IEnumerator SetUp() {
            portionSpawner.Spawn(false, transform);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            GeneratePuzzleSolutions();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            SetUpUiForSolutions();
        }

        private void GeneratePuzzleSolutions() {
            CullResultantsList();
            List<PortionType> availableTypes = new List<PortionType>(spawnedTypes);

            while(availableTypes.Count > 1) {
                MixerSolution solution  = new MixerSolution();
                InsertMixerType(ref solution, ref availableTypes);
                InsertMixerType(ref solution, ref availableTypes);
                AddSolution(ref solution);
                availableTypes.Add(solution.resultantType);
            }
            chemLock.SetFinalPortionType(solutions[solutions.Count - 1].resultantType);
        }

        private void CullResultantsList() {
            for (int i = 0; i < Enum.GetValues(typeof(PortionType)).Length; i++)
                availableResultants.Add((PortionType)i);
            
            foreach(PortionType portionType in spawnedTypes)
                availableResultants.Remove(portionType);
        }

        private void InsertMixerType(ref MixerSolution solution, ref List<PortionType> available) {
            int rand = Random.Range(0, available.Count);
            solution.mixTypes.Add(available[rand]);
            available.RemoveAt(rand);
        }

        private void AddSolution(ref MixerSolution solution) {
            int rand = Random.Range(0, availableResultants.Count);
            solution.resultantType = availableResultants[rand];
            availableResultants.RemoveAt(rand);
            solutions.Add(solution);
        }

        private void SetUpUiForSolutions() {
            foreach (MixerSolution solution in solutions)
                SetUpPad(solution);
        }

        private void SetUpPad(MixerSolution solution) {
            int rand = Random.Range(0, solutionPads.Count);
            string textValue = $"{solution.mixTypes[0]} + {solution.mixTypes[1]} = {solution.resultantType}";
            solutionPads[rand].UpdateText(textValue);
            solutionPads.RemoveAt(rand);
        }

        public List<PortionType> GetSpawnedTypes() {
            return spawnedTypes;
        }

        public void AddToSpawnedList(PortionType portionType) {
            spawnedTypes.Add(portionType);
        }

        public List<MixerSolution> GetSolutions() {
            return solutions;
        }
    }

    [System.Serializable]
    public class MixerSolution 
    {
        public List<PortionType> mixTypes;
        public PortionType resultantType;

        public MixerSolution() {
            mixTypes = new List<PortionType>();
        }
    }
}