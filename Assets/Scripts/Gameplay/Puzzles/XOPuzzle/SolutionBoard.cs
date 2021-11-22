using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class SolutionBoard : MonoBehaviour, ISolutionBoard
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] Transform boardForward;
        [SerializeField] List<Transform> textSpots;
        [SerializeField] string textObjPoolTag;
        [SerializeField] int maxEmpties = 3;
        private int currenEmpties;
        private List<TextMeshProUGUI> texts;
        private TextMeshProUGUI forwardText;
        private bool deliutional;

        private void Start() {
            deliutional = false;
            eventCollection.twistVision.AddListener(TwistVision);
            eventCollection.fixVision.AddListener(BackToNormalVision);
        }

        private void OnDestroy() {
            if(forwardText != null) {
                forwardText.gameObject.SetActive(false);
                if(texts != null) {
                    foreach (TextMeshProUGUI t in texts)
                        t.gameObject.SetActive(false);
                }
            }
            eventCollection.twistVision.RemoveListener(TwistVision);
            eventCollection.fixVision.RemoveListener(BackToNormalVision);
        }

        private void TwistVision() {
            deliutional = true;
            FlipXO();
        }

        private void BackToNormalVision() {
            deliutional = false;
            FlipXO();
        }

        private void FlipXO() {
            foreach (TextMeshProUGUI t in texts) {
                if(t.text == "X")
                    t.text = "O";
                else if(t.text == "O")
                    t.text = "X";
            }
        }

        public void SetUpBoard() {
            texts = new List<TextMeshProUGUI>();
            foreach(Transform spot in textSpots) {
                TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, spot.position, spot.rotation).GetComponent<TextMeshProUGUI>();
                texts.Add(text);
            }
            forwardText = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, boardForward.position, boardForward.rotation).GetComponent<TextMeshProUGUI>();
            forwardText.text = "↑";
        }

        public List<string> BuildNewSolution(Transform newSpot) {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            RotatePad();
            forwardText.transform.position = boardForward.position;
            forwardText.transform.rotation = boardForward.rotation;
            return SetSolution();
        }

        private void RotatePad() {
            int rotateDecider = KnotRandom.theRand.Next(0, 100);
            if(rotateDecider < 20)
                transform.Rotate(0, 0, 90);
            else if(rotateDecider > 50 && rotateDecider < 75)
                transform.Rotate(0, 0, -90);
            else if(rotateDecider < 90)
                transform.Rotate(0, 0, 180);
        }

        private List<string> SetSolution() {
            List<string> solution = new List<string>();
            currenEmpties = 0;
            for (int i = 0; i < texts.Count; i++) {
                texts[i].text = GetXOE();
                solution.Add(texts[i].text);
                texts[i].transform.position = textSpots[i].position;        
                texts[i].transform.rotation = textSpots[i].rotation;        
            }
            if(deliutional)
                FlipXO();
            return solution;
        }

        private string GetXOE() {
            int rand = KnotRandom.theRand.Next(0, 3);
            switch (rand) {
                case 0:
                    return "X";
                case 1: {
                    if(currenEmpties < maxEmpties) {
                        currenEmpties++;
                        return "";
                    } else {
                        int newRand = KnotRandom.theRand.Next(0, 2);
                        if(newRand == 0)
                            return "O";
                        else
                            return "X";
                    }
                }
                case 2:
                    return "O";
            }
            return "";
        }
    }
}