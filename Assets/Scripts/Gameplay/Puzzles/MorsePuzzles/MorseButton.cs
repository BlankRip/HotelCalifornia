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
        public char myValue;
        [SerializeField] Transform textPos;
        private TextMeshProUGUI text;
        bool inverse;

        private void Start()
        {
            myValue = 'A';
            text.text = myValue.ToString();
        }

        private void TextSetUp() {

            text.transform.position = textPos.transform.position;
            text.transform.rotation = textPos.transform.rotation;
        }

        public void Interact()
        {
            CycleValue();
        }

        private void CycleValue()
        {
            // if (myClip >= ClipName.MorseC)
            //     inverse = true;
            // else if (myClip <= ClipName.MorseA)
            //     inverse = false;
            if (!inverse)
                myValue++;
            else
                myValue--;
            text.text = myValue.ToString();
        }

        public void ShowInteractInstruction() {}
        public void HideInteractInstruction() {}
    }
}