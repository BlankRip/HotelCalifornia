using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public class Mixer : MonoBehaviour, IMixer
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] Transform successPoint;
        [SerializeField] GameObject portionPrefab;
        List<IPortion> potions = new List<IPortion>();
        List<IMixerSlot> slots = new List<IMixerSlot>();
        bool mixing;

        public void AddPotion(IPortion type, IMixerSlot slot)
        {
            potions.Add(type);
            slots.Add(slot);
        }

        public void StartMix()
        {
            if(!mixing && potions.Count == 2)
                StartCoroutine(Mix());
        }

        private IEnumerator Mix() {
            mixing = true;
            AudioPlayer.instance.PlayAudio2DOneShot(ClipName.ChemicalMixing);
            yield return new WaitForSeconds(5);
            FinishMix();
            AudioPlayer.instance.Stop2DAudio();
            mixing = false;
        }

        private void FinishMix() {
            List<MixerSolution> allSolutions = chemRoom.manager.GetSolutions();
            foreach(MixerSolution solution in allSolutions) {
                if(!solution.mixTypes.Contains(potions[0].GetPortionType()))
                    continue;
                if(!solution.mixTypes.Contains(potions[1].GetPortionType()))
                    continue;
                
                IPortion resultantPortion = GameObject.Instantiate(portionPrefab, successPoint.position, Quaternion.identity).GetComponent<IPortion>();
                resultantPortion.SetPortionType(solution.resultantType);
                foreach(IMixerSlot portion in slots)
                    portion.DestroyItemInSlot();

                potions.Clear();
                slots.Clear();
                return;
            }
        }

        public void RemovePortion(IPortion type, IMixerSlot slot) {
            potions.Remove(type);
            slots.Remove(slot);
        }

        public bool IsMixing() {
            return mixing;
        }
    }
}