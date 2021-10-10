using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class SolutionBoard : MonoBehaviour, ISolutionBoard
    {
        [SerializeField] List<Transform> textSpots;
        [SerializeField] string textObjPoolTag;
        [SerializeField] int maxEmpties = 3;
        private int currenEmpties;
        private List<TextMeshProUGUI> texts;

        public void SetUpBoard() {
            texts = new List<TextMeshProUGUI>();
            foreach(Transform spot in textSpots) {
                TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, spot.position, spot.rotation).GetComponent<TextMeshProUGUI>();
                texts.Add(text);
            }
        }

        public List<string> BuildNewSolution(Transform newSpot) {
            transform.position = newSpot.position;
            transform.rotation = newSpot.rotation;
            return SetSolution();
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
            return solution;
        }

        private string GetXOE() {
            int rand = Random.Range(0, 3);
            switch (rand) {
                case 0:
                    return "X";
                case 1: {
                    if(currenEmpties < maxEmpties) {
                        currenEmpties++;
                        return "";
                    } else {
                        int newRand = Random.Range(0, 2);
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