using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.Abilities;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleBoard : MonoBehaviour, IInterfear
    {
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        [SerializeField] ScriptableRiddleCollection riddleCollection;
        [SerializeField] RiddleSolutionPad mySolutionPad;
        private TextMeshProUGUI myText;
        private Riddle myRiddle;

        private RiddlerInputPanel inputPanel;
        private bool underSpell;
        private bool timerOn;
        private float timer;
        private float spellDuration = 15;

        private void Start() {
            inputPanel = FindObjectOfType<RiddlerInputPanel>();
            SetUpBoard();
        }

        private void SetUpBoard() {
            myRiddle = riddleCollection.GetRandomRiddle();
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myRiddle.riddle;
            mySolutionPad.SetSolution(myRiddle.answer, inputPanel);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= spellDuration) {
                    timerOn = false;
                    underSpell = false;
                    myText.text = myRiddle.riddle;
                }
            }
        }

        public void ChangeText(string value) {
            StartInterfere(value);
        }

        private void StartInterfere(string value) {
            myText.text = value;
            timer = 0;
            timerOn = true;
            underSpell = true;
        }

        public bool CanInterfear() {
            return !underSpell;
        }

        public void Interfear() {
            inputPanel.OpenPanel(this);
        }
    }
}