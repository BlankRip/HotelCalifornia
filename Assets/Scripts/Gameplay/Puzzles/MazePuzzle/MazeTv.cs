using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.Abilities;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Maze {
    public class MazeTv : MonoBehaviour, IInterfear
    {
        [SerializeField] ScriptableMazeManager maze;
        [SerializeField] string poolTag;
        [SerializeField] Transform objPosition;
        private bool fakeStatic;
        private float fakeTime = 15;
        private float timer;
        private DataToSend dataToSend;

        private void Start() {
            ObjectPool.instance.SpawnPoolObj(poolTag, objPosition.position, objPosition.rotation);

            dataToSend = new DataToSend();
            if(!DevBoy.yes)
                NetUnityEvents.instance.mazeTvEvent.AddListener(RecieveData);
        }

        private void SendData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void RecieveData(string recieved) {
            timer = 0;
            maze.manager.SetStaticObjState(true);
            fakeStatic = true;
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.mazeTvEvent.RemoveListener(RecieveData);
        }

        private void Update() {
            if(fakeStatic) {
                timer += Time.deltaTime;
                if(timer >= fakeTime) {
                    timer = 0;
                    fakeStatic = false;
                    maze.manager.SetStaticObjState(false);
                }
            }
        }

        public bool CanInterfear() {
            return !fakeStatic;
        }

        public void Interfear() {
            maze.manager.SetStaticObjState(true);
            fakeStatic = true;
            if(!DevBoy.yes)
            SendData();
        }

        private class DataToSend {
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend() {
                this.eventName = "interfereTv";
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}