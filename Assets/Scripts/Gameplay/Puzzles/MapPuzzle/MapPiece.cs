using System;
using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;
using TMPro;

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
        [SerializeField] bool needsConnection;
        [SerializeField] Material activeMat, deactiveMat;
        bool myValue;
        private IMapPuzzle mapPuzzle;
        private IMapSolution mapSolution;
        MeshRenderer meshRenderer;
        [HideInInspector] public LineRenderer lineRenderer;
        TextMeshProUGUI myText;
        private void Start()
        {
            mapPuzzle = GetComponentInParent<IMapPuzzle>();
            mapSolution = GetComponentInParent<IMapSolution>();
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if (!DevBoy.yes)
                NetUnityEvents.instance.mapPieceEvent.AddListener(RecieveData);
            meshRenderer = GetComponent<MeshRenderer>();
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = activeMat;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            //TEXT POS -0.1144269
            myText = ObjectPool.instance.SpawnPoolObj("mapText", transform.position, Quaternion.identity).GetComponent<TextMeshProUGUI>();
            myText.transform.SetParent(transform.parent);
            myText.transform.position = new Vector3(myText.transform.position.x,myText.transform.position.y, -0.1144269f);
        }

        public bool GetValue()
        {
            return myValue;
        }

        public void TurnOn()
        {
            if (meshRenderer != null)
                meshRenderer.material = activeMat;
            else
            {
                meshRenderer = GetComponent<MeshRenderer>();
                meshRenderer.material = activeMat;
            }
        }

        public void Interact()
        {
            if (needsConnection)
            {
                if (mapManager.previousPiece == null)
                    mapManager.previousPiece = this;
                else
                {
                    mapSolution.AddConnection(mapManager.previousPiece, this);
                    mapManager.previousPiece.lineRenderer.SetPositions(new Vector3[] { mapManager.previousPiece.transform.position, transform.position });
                    mapManager.previousPiece = null;
                    lineRenderer.enabled = true;
                }
            }
            else
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