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
        private IMorseDevice device;
        private MorseAlphPanel panel;
        private TextMeshProUGUI text;
        bool inverse;

        private void Start()
        {
            myValue = '0';
            device = GetComponentInParent<IMorseDevice>();
            panel = FindObjectOfType<MorseAlphPanel>();
            text = ObjectPool.instance.SpawnPoolObj("MorseText", textPos.transform.position,
                textPos.transform.rotation).GetComponent<TextMeshProUGUI>();
            text.text = myValue.ToString();
        }

        public void SetText(char value) {
            myValue = value;
            text.text = myValue.ToString();
            device.CheckSolution();
        }

        public void Interact()
        {
            panel.OpenPanel(this);
        }

        public void ShowInteractInstruction() {}
        public void HideInteractInstruction() {}
    }
}