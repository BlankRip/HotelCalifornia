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

        void Start()
        {
            solution = morsePuzzle.manager.GetSolution();
            TextSetUp();
        }

        private void TextSetUp() {
            TextMeshProUGUI text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[0].transform.position,
                alphaBetaOmagaTextPos[0].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "α";
            text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[1].transform.position,
                alphaBetaOmagaTextPos[1].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "ß";
            text = ObjectPool.instance.SpawnPoolObj("MorseText", alphaBetaOmagaTextPos[2].transform.position,
                alphaBetaOmagaTextPos[2].transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = "Ω";
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
            puzzleTracker.tracker.OnePuzzleSolved();
            foreach(MorseButton button in buttons)
            {
                button.tag = "Untagged";
                button.gameObject.layer = 0;
            }
        }
    }
}