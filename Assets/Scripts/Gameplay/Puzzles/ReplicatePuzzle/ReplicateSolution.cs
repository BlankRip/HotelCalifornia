using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateSolution : MonoBehaviour, IReplicateSolution
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] ReplicateObjectDatabase replicateObjectDatabase;
        [SerializeField] List<Transform> objectSpots;
        [SerializeField] ScriptableLevelSeed scriptableLevelSeed;
        private bool screwed;
        List<GameObject> storedObjs = new List<GameObject>();
        List<RepObj> storedRepObjs = new List<RepObj>();
        List<ReplicateObjectSlot> slots = new List<ReplicateObjectSlot>();
        List<bool> filledSpotValues = new List<bool>();
        System.Random random;

        private void Awake()
        {
            AssignSlots();
        }

        private void Start()
        {
            screwed = false;
            if (random == null)
                random = new System.Random(scriptableLevelSeed.levelSeed.GetSeed());
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
            if (screwed)
            {
                for (int i = 0; i < storedObjs.Count; i++)
                {
                    MeshRenderer[] mrs = storedObjs[i].GetComponentsInChildren<MeshRenderer>();
                    foreach(MeshRenderer mr in mrs)
                    {
                        mr.enabled = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < storedObjs.Count; i++)
                {
                    MeshRenderer[] mrs = storedObjs[i].GetComponentsInChildren<MeshRenderer>();
                    foreach(MeshRenderer mr in mrs)
                    {
                        mr.enabled = true;
                    }
                }
            }
        }

        public List<string> BuildNewSolution(Transform newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
        }


        void SelectFilledSpots()
        {
            if (random == null)
                random = new System.Random(scriptableLevelSeed.levelSeed.GetSeed());

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
                    Destroy(go.GetComponent<Rigidbody>());
                    go.layer = 0;
                    go.tag = "Untagged";
                    repObj.SetOriginal(objectSpots[i].position, objectSpots[i].rotation);
                    storedObjs.Add(go);
                    storedRepObjs.Add(repObj);
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

        public List<RepObj> GetStoredRepObjs()
        {
            return storedRepObjs;
        }

        public ReplicateObjectSlot GetCorrespondingSlot(int index)
        {
            return slots[index];
        }
    }
}