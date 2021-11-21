using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

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
        private TextMeshProUGUI myText;

        DataToSend dataToSend;

        private void Start() {
            solution = lightLever.manager.GetSolution();
            panel = FindObjectOfType<NumberPadPanel>();
            SetUpText();

            dataToSend = new DataToSend();
            if(!DevBoy.yes)
                NetUnityEvents.instance.lightsNumberPad.AddListener(RecieveData);
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            CheckSolutionOffline(extracted.expectedSolution);
        }

        private void SendData(List<int> expected) {
            dataToSend.expectedSolution = expected;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(myText != null)
                myText.gameObject.SetActive(false);
            if(!DevBoy.yes)
                NetUnityEvents.instance.lightsNumberPad.RemoveListener(RecieveData);
        }
        
        private void SetUpText() {
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = lightLever.manager.GetColorHelper();
        }

        public bool CheckSolution(List<int> toCheck) {
            if(!DevBoy.yes)
                SendData(toCheck);
            return CheckSolutionOffline(toCheck);
        }

        private bool CheckSolutionOffline(List<int> toCheck) {
            if(!solved) {
                for (int i = 0; i < solution.Count; i++) {
                    if(toCheck[i] != solution[i])
                        return false;
                }
                solved = true;
                puzzleTracker.tracker.OnePuzzleSolved();
                Debug.Log("Solved");
                return true;
            }
            return false;
        }

        public void ShowInteractInstruction() {
            if(!solved)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Interact");
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        public void Interact() {
            if(!solved)
                panel.OpenPanel(this);
        }

        private class ExtractionClass {
            public List<int> expectedSolution;
        }

        private class DataToSend {
            public List<int> expectedSolution;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend() {
                eventName = "lightsNumPad";
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}
