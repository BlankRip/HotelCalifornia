using System;
using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.UI;

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
        [SerializeField] Material activeMat, deactiveMat;
        bool myValue;
        private IMapPuzzle mapPuzzle;
        private IMapSolutionRoom mapSolutionRoom;
        MeshRenderer meshRenderer;
        TextMeshProUGUI myText;
        public bool interactable = true;

        private void Start()
        {
            mapPuzzle = GetComponentInParent<IMapPuzzle>();
            mapSolutionRoom = transform.parent.GetComponentInParent<IMapSolutionRoom>();
            screwed = false;
            eventCollection.twistVision.AddListener(Messup);
            eventCollection.fixVision.AddListener(Fix);
            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if (!DevBoy.yes)
                NetUnityEvents.instance.mapPieceEvent.AddListener(RecieveData);
            if (mapManager.thePuzzle == null)
                mapManager.thePuzzle = mapPuzzle;
            if (mapManager.theSolution == null)
                mapManager.theSolution = mapSolutionRoom;
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void OnDestroy()
        {
            if (!DevBoy.yes)
                NetUnityEvents.instance.mapPieceEvent.RemoveListener(RecieveData);
                mapManager.thePuzzle = null;
                mapManager.theSolution = null;
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
            if (interactable)
            {
                CycleValue();
                if(!DevBoy.yes)
                SendData();
            }
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

            mapManager.thePuzzle.CheckSolution();
            mapManager.theSolution.CheckSolution();
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
                myText.gameObject.SetActive(false);
            }
            else
            {
                meshRenderer.enabled = true;
                myText.gameObject.SetActive(true);
            }
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        public void ShowInteractInstruction() {
            if(interactable)
                InstructionText.instance.ShowInstruction("Press \'LMB\' To Toggle Node");
        }

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