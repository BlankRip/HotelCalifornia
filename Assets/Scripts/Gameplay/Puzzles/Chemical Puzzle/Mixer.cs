using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom
{
    public class Mixer : MonoBehaviour, IMixer
    {
        public FinalPotions mySolution;
        List<PortionType> slots = new List<PortionType>();
        public Transform[] returnSlots;
        List<GameObject> savedPotions = new List<GameObject>();

        public void AddPotion(PortionType type, GameObject obj)
        {
            slots.Add(type);
            savedPotions.Add(obj);
            Debug.LogError($"ADDING <color={type.ToString()}>{type.ToString()}</color>");
            if (slots.Count == 2)
                Invoke("Mix", 0.2f);
        }

        public void Mix()
        {
            if (mySolution.CompareParts(slots[0], slots[1]))
            {
                Debug.LogError("CORRECT CONCOCTION!");
                Destroy(gameObject);
            }
            else
            {
                savedPotions[0].transform.position = returnSlots[0].position;
                savedPotions[0].SetActive(true);
                savedPotions[1].transform.position = returnSlots[1].position;
                savedPotions[1].SetActive(true);
                Debug.LogError("WRONG CONCOCTION!!!");
                slots.Clear();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Potion") == true)
            {
                AddPotion(other.GetComponent<PortionObj>().GetPortionType(), other.gameObject);
                other.GetComponent<testey>().Drop();
                other.gameObject.SetActive(false);
            }
        }
    }
}