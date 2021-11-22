using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;
using Knotgames.Gameplay.UI;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Riddler {
    public class RiddleSolutionPad : MonoBehaviour, IInteractable
    {
        private static int riddlePadId;
        public static void ResetID() {
            riddlePadId = 0;
        }

        [SerializeField] ScriptableRiddlerPuzzle thePuzzle;
        private RiddlerInputPanel inputPanel;
        private string mySolution;
        private bool solved;

        private int myId;
        private bool inUse;
        private DataToSend dataToSend;

        private void Start() {
            myId = riddlePadId;
            riddlePadId++;
            dataToSend = new DataToSend(myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.riddlePadEvent.AddListener(RecieveData);
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId) {
                inUse = extracted.inUse;
                if(!inUse)
                    CheckSolve(extracted.input);
            }
        }

        private void SendData(bool value, string inputValue) {
            dataToSend.inUse = value;
            dataToSend.input = inputValue;
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.riddlePadEvent.RemoveListener(RecieveData);
        }

        public void SetSolution(string solution, RiddlerInputPanel panel) {
            mySolution = solution.ToLower();
            inputPanel = panel;
        }

        public bool Check(string value) {
            if(!DevBoy.yes)
                SendData(false, value);
            return CheckSolve(value);
        }

        private bool CheckSolve(string value) {
            if(value.ToLower() == mySolution) {
                solved = true;
                thePuzzle.manager.UpdateSolve();
                GetComponent<Renderer>().material = null;
                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.RightAnswer);
                return true;
            } else {
                AudioPlayer.instance.PlayAudio2DOneShot(ClipName.WrongAnswer);
                return false;
            }
        }

        public void Interact() {
            if(!solved && !inUse) {
                inputPanel.OpenPanel(this);
                if(!DevBoy.yes)
                    SendData(true, "");
            }
        }

        public void ShowInteractInstruction() {
            if(!solved && !inUse)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Interact");
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        private class ExtractionClass {
            public int myId;
            public bool inUse;
            public string input;
        }

        private class DataToSend {
            public int myId;
            public bool inUse;
            public string input;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id) {
                eventName = "riddlePad";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}