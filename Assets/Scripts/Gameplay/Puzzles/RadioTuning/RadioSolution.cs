using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public class RadioSolution : MonoBehaviour, IRadioSolution
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] List<Transform> textSpots;
        [SerializeField] string textObjPoolTag;
        private List<TextMeshProUGUI> texts;
        private bool screwed;

        private void Start()
        {
            screwed = false;
            eventCollection.twistVision.AddListener(MessupFrequency);
            eventCollection.fixVision.AddListener(NormalFrequency);
        }

        private void OnDestroy()
        {
            if(texts != null) {
                foreach (TextMeshProUGUI t in texts)
                    t.gameObject.SetActive(false);
            }
            eventCollection.twistVision.RemoveListener(MessupFrequency);
            eventCollection.fixVision.RemoveListener(NormalFrequency);
        }

        private void MessupFrequency()
        {
            screwed = true;
            FlipFrequencies();
        }

        private void NormalFrequency()
        {
            screwed = false;
            FlipFrequencies();
        }

        private void FlipFrequencies()
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

        public void SetupRadio()
        {
            texts = new List<TextMeshProUGUI>();
            foreach (Transform spot in textSpots)
            {
                TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, spot.position, spot.rotation).GetComponent<TextMeshProUGUI>();
                texts.Add(text);
            }
        }

        public List<string> BuildNewSolution(Transform newSpot)
        {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
        }

        private List<string> SetSolution()
        {
            List<string> solution = new List<string>();
            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].text = GetFrequency();
                solution.Add(texts[i].text);
                texts[i].transform.position = textSpots[i].position;
                texts[i].transform.rotation = textSpots[i].rotation;
            }
            if (screwed)
                FlipFrequencies();
            return solution;
        }

        private string GetFrequency()
        {
            int rand = KnotRandom.theRand.Next(0, 5);
            switch (rand)
            {
                case 0:
                    return "90Hz";
                case 1:
                    return "45Hz";
                case 2:
                    return "0Hz";
                case 3:
                    return "-45Hz";
                case 4:
                    return "-90Hz";
            }
            return "";
        }
    }
}