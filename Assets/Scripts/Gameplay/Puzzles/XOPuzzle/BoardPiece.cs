using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.XO {
    public class BoardPiece : MonoBehaviour, IInteractable
    {
        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] string textPoolTag;
        private List<string> values;
        private int index;
        private TextMeshProUGUI myText;
        private IPuzzleBoard board;
        private bool delusional;
        
        private void Start() {
            BasicSetUp();
            board = GetComponentInParent<IPuzzleBoard>();

            delusional = false;
            eventCollection.twistVision.AddListener(TwistVision);
            eventCollection.fixVision.AddListener(BackToNormalVision);
        }

        private void OnDestroy() {
            if(delusional)
                FlipXO();
            eventCollection.twistVision.RemoveListener(TwistVision);
            eventCollection.fixVision.RemoveListener(BackToNormalVision);
        }

        private void TwistVision() {
            delusional = true;
            FlipXO();
        }

        private void BackToNormalVision() {
            delusional = false;
            FlipXO();
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
            if(delusional)
                FlipXO();
            board.CheckSolution();
        }

        private void FlipXO() {
            if(myText.text == "X")
                myText.text = "O";
            else if(myText.text == "O")
                myText.text = "X";
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