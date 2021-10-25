using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class NumberPad : MonoBehaviour, IInteractable
    {
        [SerializeField] ScriptablePuzzleStatusTracker puzzleTracker;
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        private NumberPadPanel panel;
        private List<int> solution;
        private bool solved = false;

        private void Start() {
            solution = lightLever.manager.GetSolution();
            panel = FindObjectOfType<NumberPadPanel>();
            SetUpText();
        }
        
        private void SetUpText() {
            TextMeshProUGUI myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = lightLever.manager.GetColorHelper();
        }

        public void CheckSolution(List<int> toCheck) {
            for (int i = 0; i < solution.Count; i++) {
                if(toCheck[i] != solution[i])
                    return;
            }
            solved = true;
            puzzleTracker.tracker.OnePuzzleSolved();
            Debug.Log("Solved");
        }

        public void ShowInteractInstruction() {

        }

        public void HideInteractInstruction() {

        }

        public void Interact() {
            if(!solved)
                panel.OpenPanel(this);
        }
    }
}
