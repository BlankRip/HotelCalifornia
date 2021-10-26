using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapSolution : MonoBehaviour, IMapSolution
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] List<Transform> textSpots;
        [SerializeField] string textObjPoolTag;
        private List<TextMeshProUGUI> texts;
        private bool screwed;

        private void Start()
        {
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
        }

        private void OnDestroy()
        {
            eventCollection.twistVision.RemoveListener(Messup);
            eventCollection.fixVision.RemoveListener(Fix);
        }

        private void Messup()
        {
            screwed = true;
            FlipValues();
        }

        private void Fix()
        {
            screwed = false;
            FlipValues();
        }

        private void FlipValues()
        {
            foreach (TextMeshProUGUI t in texts)
            {
                switch (t.text)
                {
                    case "90Hz":
                        t.text = "-90Hz";
                        break;
                    case "45Hz":
                        t.text = "-45Hz";
                        break;
                    case "0Hz":
                        t.text = "0Hz";
                        break;
                    case "-45Hz":
                        t.text = "45Hz";
                        break;
                    case "-90Hz":
                        t.text = "90Hz";
                        break;
                }
            }
        }

        public void SetupMap()
        {
            texts = new List<TextMeshProUGUI>();
            foreach (Transform spot in textSpots)
            {
                TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, spot.position, spot.rotation).GetComponent<TextMeshProUGUI>();
                texts.Add(text);
            }
        }

        public List<bool> BuildNewSolution(Transform newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
        }

        private List<bool> SetSolution()
        {
            List<bool> solution = new List<bool>();
            for (int i = 0; i < texts.Count; i++)
            {
                // texts[i].text = GetFrequency();
                // solution.Add(texts[i].text);
                // texts[i].transform.position = textSpots[i].position;
                // texts[i].transform.rotation = textSpots[i].rotation;
            }
            if (screwed)
                FlipValues();
            return solution;
        }
    }
}