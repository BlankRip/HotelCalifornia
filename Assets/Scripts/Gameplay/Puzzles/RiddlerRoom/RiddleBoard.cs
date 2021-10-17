using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleBoard : MonoBehaviour
    {
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        [SerializeField] ScriptableRiddleCollection riddleCollection;
        [SerializeField] RiddleSolutionPad mySolutionPad;
        private TextMeshProUGUI myText;
        private Riddle myRiddle;

        private void Start() {
            riddleCollection.SetUpForNewRiddleSet();
        }

        private void SetUpBoard() {
            myRiddle = riddleCollection.GetRandomRiddle();
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myRiddle.riddle;
            mySolutionPad.SetSolution(myRiddle.answer);
        }
    }
}