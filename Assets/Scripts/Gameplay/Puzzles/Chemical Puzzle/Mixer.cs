using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public class Mixer : MonoBehaviour, IMixer
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] Transform successPoint;
        [SerializeField] GameObject portionPrefab;
        List<IPortion> slotted = new List<IPortion>();
        List<IMixerSlot> savedPotions = new List<IMixerSlot>();
        bool mixing;

        public void AddPotion(IPortion type, IMixerSlot slot)
        {
            slotted.Add(type);
            savedPotions.Add(slot);
        }

        public void StartMix()
        {
            if(!mixing && slotted.Count == 2)
                StartCoroutine(Mix());
        }

        private IEnumerator Mix() {
            mixing = true;
            yield return new WaitForSeconds(5);
            FinishMix();
            mixing = false;
        }

        private void FinishMix() {
            List<MixerSolution> allSolutions = chemRoom.manager.GetSolutions();
            foreach(MixerSolution solution in allSolutions) {
                if(!solution.mixTypes.Contains(slotted[0].GetPortionType()))
                    continue;
                if(!solution.mixTypes.Contains(slotted[1].GetPortionType()))
                    continue;
                
                IPortion resultantPortion = GameObject.Instantiate(portionPrefab, successPoint.position, Quaternion.identity).GetComponent<IPortion>();
                resultantPortion.SetPortionType(solution.resultantType);
                foreach(IMixerSlot portion in savedPotions)
                    portion.DestroyItemInSlot();

                slotted.Clear();
                savedPotions.Clear();
                return;
            }
        }

        public void RemovePortion(IPortion type, IMixerSlot slot) {
            slotted.Remove(type);
            savedPotions.Remove(slot);
        }

        public bool IsMixing() {
            return mixing;
        }
    }
}