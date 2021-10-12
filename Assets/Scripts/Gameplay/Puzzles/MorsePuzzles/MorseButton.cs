using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorseButton : MonoBehaviour, IInteractable
    {
        public ClipName myClip;
        [SerializeField] TextMeshProUGUI text;
        bool inverse;

        private void Start()
        {
            myClip = ClipName.MorseA;
            text.text = myClip.ToString();
        }

        public void Interact()
        {
            CycleValue();
        }

        private void CycleValue()
        {
            if (myClip >= ClipName.MorseC)
                inverse = true;
            else if (myClip <= ClipName.MorseA)
                inverse = false;
            if (!inverse)
                myClip++;
            else
                myClip--;
            text.text = myClip.ToString();
        }

        public void ShowInteractInstruction() {}
        public void HideInteractInstruction() {}
    }
}