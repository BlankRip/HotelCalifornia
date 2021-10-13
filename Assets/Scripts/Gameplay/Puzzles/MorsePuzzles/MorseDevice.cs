using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseDevice : MonoBehaviour, IMorseDevice
    {
        public List<ClipName> solution;
        MorsePlayer player;
        [SerializeField] MorseButton[] buttons;

        void Start()
        {
            player = FindObjectOfType<MorsePlayer>();
        }

        public bool CheckSolution()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if(solution[i]!= buttons[i].myClip)
                    return false;
            }
            return true;
        }

        public void SetSolution(List<char> solution) {
            
        }

        public void Solved()
        {
            player.Solved();
            foreach(MorseButton button in buttons)
            {
                button.tag = "Untagged";
                button.gameObject.layer = 0;
            }
        }
    }
}