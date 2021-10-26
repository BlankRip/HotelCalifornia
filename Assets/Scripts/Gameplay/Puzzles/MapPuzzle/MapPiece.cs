using System;
using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Map
{
    public class MapPiece : MonoBehaviour, IMapPiece, IInteractable
    {
        [SerializeField] MapManager mapManager;
        public static int pieceId;
        public static void ResetIDs()
        {
            pieceId = 0;
        }

        [SerializeField] GameplayEventCollection eventCollection;
        private bool screwed;
        private int id;
        private DataToSend dataToSend;
        bool needsConnection;
        [SerializeField] Material activeMat, deactiveMat;
        bool myValue;
        private IMapPuzzle mapPuzzle;
        MeshRenderer meshRenderer;
        private void Start()
        {
            mapPuzzle = GetComponentInParent<IMapPuzzle>();
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if (!DevBoy.yes)
                NetUnityEvents.instance.mapPieceEvent.AddListener(RecieveData);
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public bool GetValue()
        {
            return myValue;
        }

        public void Interact()
        {
            if (needsConnection)
                if (mapManager.previousPiece == null)
                    mapManager.previousPiece = this;
                else
                    Debug.Log("x");//TODO Connect pieces
                
            CycleValue();
            SendData();
        }

        private void CycleValue()
        {
            if (myValue)
            {
                myValue = false;
                meshRenderer.material = deactiveMat;
            }
            else
            {
                myValue = true;
                meshRenderer.material = activeMat;
            }

            if (screwed)
                FlipValues();
            mapPuzzle.CheckSolution();
        }

        private void Messup()
        {
            screwed = true;
            FlipValues();
        }

        private void Fix()
        {
            screwed = false;
            FlipValues();
        }

        private void FlipValues()
        {
            if (myValue)
                meshRenderer.material = deactiveMat;
            else
                meshRenderer.material = activeMat;
        }

        public void HideInteractInstruction() { }

        public void ShowInteractInstruction() { }

        private void SendData()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.myId == id)
                CycleValue();
        }

        private class ExtractionClass
        {
            public int myId;
        }

        private class DataToSend
        {
            public int myId;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(int id)
            {
                eventName = "MapPiece";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}