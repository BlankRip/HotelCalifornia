using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.UI {
    public class InstructionText : MonoBehaviour
    {
        public static InstructionText instance;
        private TextMeshProUGUI theText;

        private void Awake() {
            if(instance == null) {
                instance = this;
                theText = GetComponent<TextMeshProUGUI>();
                this.gameObject.SetActive(false);
            }
        }
        public void ShowInstruction(string instruction) {
            theText.text = instruction;
            this.gameObject.SetActive(true);
        }

        public void HideInstruction() {
            this.gameObject.SetActive(false);
        }
    }
}