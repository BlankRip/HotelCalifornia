using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorsePuzzle : MonoBehaviour, IMorsePuzzle
    {
        public List<ClipName> solution;
        MorsePlayer player;
        [SerializeField] MorseButton[] buttons;

        void Start()
        {
            player = FindObjectOfType<MorsePlayer>();
            player.Link(this);
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