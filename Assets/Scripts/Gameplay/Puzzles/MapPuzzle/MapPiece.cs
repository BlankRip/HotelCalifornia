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
        private IMapSolutionRoom mapSolutionRoom;
        MeshRenderer meshRenderer;
        [HideInInspector] public LineRenderer lineRenderer;
        TextMeshProUGUI myText;
        private void Start()
        {
            mapPuzzle = GetComponentInParent<IMapPuzzle>();
            mapSolution = GetComponentInParent<IMapSolution>();
            mapSolutionRoom = transform.parent.GetComponentInParent<IMapSolutionRoom>();
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if (!DevBoy.yes)
                NetUnityEvents.instance.mapPieceEvent.AddListener(RecieveData);
            meshRenderer = GetComponent<MeshRenderer>();
            if (mapSolutionRoom != null)
                SetupLR(UnityEngine.Random.Range(0, 8));
        }

        public void SetupLR(int i)
        {
            if (lineRenderer == null)
            {
                // Debug.LogError("LR");
                lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.material = mapManager.materials[i];
                lineRenderer.startWidth = 0.05f;
                lineRenderer.endWidth = 0.005f;
            }
        }

        public void Setuptext(string value)
        {
            //TEXT POS -0.1144269
            Vector3 textPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.05f);
            myText = ObjectPool.instance.SpawnPoolObj("mapText", textPos, Quaternion.identity).GetComponent<TextMeshProUGUI>();
            myText.text = value;
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
                    if (mapSolutionRoom != null)
                        mapSolutionRoom.CheckMySol();
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

            if (mapPuzzle != null)
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

        public void FlipValues()
        {
            if (screwed)
            {
                meshRenderer.enabled = false;
                if (lineRenderer != null)
                    lineRenderer.enabled = false;
                myText.gameObject.SetActive(false);
            }
            else
            {
                meshRenderer.enabled = true;
                if (lineRenderer != null)
                    lineRenderer.enabled = true;
                myText.gameObject.SetActive(true);
            }
        }

        public void HideInteractInstruction() { }

        public void ShowInteractInstruction() { }

        private void SendData()
        {
            if (!DevBoy.yes)
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