using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseDevice : MonoBehaviour, IMorseDevice
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] ScriptableMorsePuzzle morsePuzzle;
        [SerializeField] MorseButton[] buttons;
        [SerializeField] List<Transform> alphaBetaOmagaTextPos;
        private List<char> solution;
        private MorseAlphPanel panel;
        private List<GameObject> myTextObjs;

        void Start()
        {
            solution = morsePuzzle.manager.GetSolution();
            panel = FindObjectOfType<MorseAlphPanel>();
            TextSetUp();
        }

        private void OnDestroy() {
            if(myTextObjs != null) {
                foreach (GameObject go in myTextObjs)
                    go.SetActive(false);
            }
        }

        private void TextSetUp() {
            myTextObjs = new List<GameObject>();
            TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[0].transform.position,
                alphaBetaOmagaTextPos[0].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "α";
            myTextObjs.Add(text.gameObject);
            text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[1].transform.position,
                alphaBetaOmagaTextPos[1].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "ß";
            myTextObjs.Add(text.gameObject);
            text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[2].transform.position,
                alphaBetaOmagaTextPos[2].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "Ω";
            myTextObjs.Add(text.gameObject);
        }

        public void CheckSolution()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(solution[i] != buttons[i].myValue)
                    return;
            }
            Solved();
        }

        private void Solved()
        {
            Debug.Log("Solved");
            panel.gameObject.SetActive(false);
            puzzleTracker.tracker.OnePuzzleSolved();
            foreach(MorseButton button in buttons)
            {
                button.tag = "Untagged";
                button.gameObject.layer = 0;
            }
        }
    }
}