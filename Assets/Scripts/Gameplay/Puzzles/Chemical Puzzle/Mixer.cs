using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public class Mixer : MonoBehaviour, IMixer
    {
        [SerializeField] ScriptatbleChemicalPuzzle chemRoom;
        [SerializeField] Transform[] returnSlots;
        [SerializeField] Transform successPoint;
        [SerializeField] GameObject portionPrefab;
        List<IPortion> slotted = new List<IPortion>();
        List<GameObject> savedPotions = new List<GameObject>();

        public void AddPotion(IPortion type, GameObject obj)
        {
            slotted.Add(type);
            savedPotions.Add(obj);
            Debug.LogError($"ADDING <color={type.ToString()}>{type.ToString()}</color>");
            if (slotted.Count == 2)
                Invoke("Mix", 0.2f);
        }

        public void Mix()
        {
            List<MixerSolution> allSolutions = chemRoom.manager.GetSolutions();
            foreach(MixerSolution solution in allSolutions) {
                if(!solution.mixTypes.Contains(slotted[0].GetPortionType()))
                    continue;
                if(!solution.mixTypes.Contains(slotted[1].GetPortionType()))
                    continue;
                
                IPortion resultantPortion = GameObject.Instantiate(portionPrefab, successPoint.position, Quaternion.identity).GetComponent<IPortion>();
                resultantPortion.SetPortionType(solution.resultantType);
                foreach(GameObject portion in savedPotions)
                    Destroy(portion);

            }
            
            savedPotions[0].transform.position = returnSlots[0].position;
            savedPotions[0].SetActive(true);
            savedPotions[1].transform.position = returnSlots[1].position;
            savedPotions[1].SetActive(true);
            Debug.LogError("WRONG CONCOCTION!!!");
            slotted.Clear();
            savedPotions.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Potion") == true)
            {
                IPortion currentPortion = other.GetComponent<IPortion>();
                AddPotion(currentPortion, other.gameObject);
                currentPortion.Drop();
                other.gameObject.SetActive(false);
            }
        }
    }
}