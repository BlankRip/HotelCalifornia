using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseDevice : MonoBehaviour, IMorseDevice
    {
        [SerializeField] ScriptableMorsePuzzle morsePuzzle;
        [SerializeField] MorseButton[] buttons;
        private List<char> solution;

        void Start()
        {
            solution = morsePuzzle.manager.GetSolution();
        }

        public void CheckSolution()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(solution[i] != buttons[i].myValue)
                    return;
            }
            morsePuzzle.manager.Solved();
            Solved();
        }

        private void Solved()
        {
            foreach(MorseButton button in buttons)
            {
                button.tag = "Untagged";
                button.gameObject.layer = 0;
            }
        }
    }
}