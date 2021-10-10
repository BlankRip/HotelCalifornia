using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class BoardPiece : MonoBehaviour, IInteractable
    {
        [SerializeField] string textPoolTag;
        private List<string> values;
        private int index;
        private TextMeshProUGUI myText;
        private IPuzzleBoard board;
        
        private void Start() {
            BasicSetUp();
            board = GetComponentInParent<IPuzzleBoard>();
        }

        private void BasicSetUp() {
            if(myText == null) {
                values = new List<string>();
                values.Add("");
                values.Add("X");
                values.Add("O");
                myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, transform.position, transform.rotation).GetComponent<TextMeshProUGUI>();
                myText.text = values[index];
            }
        }

        public void Interact() {
            CylceValue();
        }

        private void CylceValue() {
            if(index == 2)
                index = 0;
            else
                index++;
            myText.text = values[index];
            board.CheckSolution();
        }

        public string GetValue() {
            return values[index];
        }

        public void SetRandom() {
            BasicSetUp();
            index = Random.Range(1, 3);
            myText.text = values[index];
        }

        public void ShowInteractInstruction() {
        }

        public void HideInteractInstruction() {
        }
    }
}