using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateSolution : MonoBehaviour, IReplicateSolution
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] ReplicateObjectDatabase replicateObjectDatabase;
        [SerializeField] List<Transform> objectSpots;
        private bool screwed;
        List<RepObj> storedObjs = new List<RepObj>();
        List<ReplicateObjectSlot> slots = new List<ReplicateObjectSlot>();

        private void Awake()
        {
            AssignSlots();
        }
        
        private void Start()
        {
            screwed = false;
            eventCollection.twistVision.AddListener(SwapObjects);
            eventCollection.fixVision.AddListener(ResetObjects);
        }

        void AssignSlots()
        {
            foreach (Transform spot in objectSpots)
            {
                slots.Add(spot.gameObject.GetComponent<ReplicateObjectSlot>());
            }
        }

        private void OnDestroy()
        {
            eventCollection.twistVision.RemoveListener(SwapObjects);
            eventCollection.fixVision.RemoveListener(ResetObjects);
        }

        private void SwapObjects()
        {
            screwed = true;
            FlipObjects();
        }

        private void ResetObjects()
        {
            screwed = false;
            FlipObjects();
        }

        private void FlipObjects()
        {
            if (!screwed)
            {
                for (int i = 0; i < storedObjs.Count; i++)
                {
                    if (i + 1 > storedObjs.Count)
                        storedObjs[i].Object.transform.SetPositionAndRotation(storedObjs[0].Object.transform.position, storedObjs[0].Object.transform.rotation);
                    else
                        storedObjs[i].Object.transform.SetPositionAndRotation(storedObjs[i + 1].Object.transform.position, storedObjs[i + 1].Object.transform.rotation);
                }
            }
            else
            {
                for (int i = 0; i < storedObjs.Count; i++)
                {
                    storedObjs[i].ResetToOriginal();
                }
            }
        }

        public List<string> BuildNewSolution(Transform newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
        }
        List<bool> filledSpotValues = new List<bool>();
        System.Random random = new System.Random();

        void SelectFilledSpots()
        {
            for (int i = 0; i < objectSpots.Count; i++)
            {
                bool value = random.NextDouble() > 0.5;
                filledSpotValues.Add(value);
                if (value)
                    Destroy(slots[i]);
            }
        }

        private List<string> SetSolution()
        {
            SelectFilledSpots();
            List<string> solution = new List<string>();
            for (int i = 0; i < objectSpots.Count; i++)
            {
                if (filledSpotValues[i])
                {
                    RepObj repObj = GetRepObject();
                    GameObject go = Instantiate(repObj.Object, objectSpots[i].position, objectSpots[i].rotation, this.transform);
                    Destroy(go.GetComponent<RepObjTransformSync>());
                    Destroy(go.GetComponent<ReplicateObject>());
                    go.layer = 0;
                    go.tag = "Untagged";
                    repObj.SetOriginal(objectSpots[i].position, objectSpots[i].rotation);
                    storedObjs.Add(repObj);
                    solution.Add(repObj.name);
                }
                else
                {
                    solution.Add("empty");
                }
            }
            if (screwed)
                FlipObjects();
            return solution;
        }

        private RepObj GetRepObject()
        {
            return replicateObjectDatabase.objects[Random.Range(0, replicateObjectDatabase.objects.Count)];
        }

        public List<RepObj> GetStoredObjs()
        {
            return storedObjs;
        }

        public ReplicateObjectSlot GetCorrespondingSlot(int index)
        {
            return slots[index];
        }
    }
}