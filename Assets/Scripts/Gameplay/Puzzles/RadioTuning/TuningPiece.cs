using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Radio
{
    public class TuningPiece : MonoBehaviour, IInteractable
    {
        public static int pieceId;
        public static void ResetIDs()
        {
            pieceId = 0;
        }

        [SerializeField] GameplayEventCollection eventCollection;
        [SerializeField] Transform textSpot;
        [SerializeField] string textObjPoolTag;
        private int index;
        private Quaternion originalRot;
        private IRadioTuner tuner;
        private bool screwed;
        private int id;
        private DataToSend dataToSend;
        private string myVal;

        private void Start()
        {
            SetupText();
            originalRot = transform.rotation;
            tuner = GetComponentInParent<IRadioTuner>();
            screwed = false;
            eventCollection.twistVision.AddListener(MessupFrequency);
            eventCollection.fixVision.AddListener(NormalFrequency);
            id = pieceId;
            pieceId++;
            dataToSend = new DataToSend(id);
            if (!DevBoy.yes)
                NetUnityEvents.instance.radioPieceEvent.AddListener(RecieveData);
        }

        private void RecieveData(string recieved)
        {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if (extracted.myId == id)
                CycleValue();
        }

        private void SendData()
        {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void OnDestroy()
        {
            if (screwed)
                FlipRadio();
            eventCollection.twistVision.RemoveListener(MessupFrequency);
            eventCollection.fixVision.RemoveListener(NormalFrequency);

            if (!DevBoy.yes)
                NetUnityEvents.instance.radioPieceEvent.RemoveListener(RecieveData);
        }

        private void MessupFrequency()
        {
            screwed = true;
            FlipRadio();
        }

        private void NormalFrequency()
        {
            screwed = false;
            FlipRadio();
        }

        public void Interact()
        {
            CycleValue();
            SendData();
        }

        private void CycleValue()
        {
            if (index == 4)
                index = 0;
            else
                index++;

            switch (index)
            {
                case 0:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-90, Vector3.forward);
                    myVal = "-90Hz";
                    break;
                case 1:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-45, Vector3.forward);
                    myVal = "-45Hz";
                    break;
                case 2:
                    transform.rotation = originalRot * Quaternion.AngleAxis(0, Vector3.forward);
                    myVal = "0Hz";
                    break;
                case 3:
                    transform.rotation = originalRot * Quaternion.AngleAxis(45, Vector3.forward);
                    myVal = "45Hz";
                    break;
                case 4:
                    transform.rotation = originalRot * Quaternion.AngleAxis(90, Vector3.forward);
                    myVal = "90Hz";
                    break;
            }

            text.text = myVal;
            UnityEngine.Debug.LogError($"Frequency is: {myVal}", gameObject);

            if (screwed)
                FlipRadio();
            tuner.CheckSolution();
        }

        private void FlipRadio()
        {
            switch (index)
            {
                case 4:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-90, Vector3.forward);
                    myVal = "-90Hz";

                    break;
                case 3:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-45, Vector3.forward);
                    myVal = "-45Hz";
                    break;
                case 2:
                    transform.rotation = originalRot * Quaternion.AngleAxis(0, Vector3.forward);
                    myVal = "0Hz";
                    break;
                case 1:
                    transform.rotation = originalRot * Quaternion.AngleAxis(45, Vector3.forward);
                    myVal = "45Hz";
                    break;
                case 0:
                    transform.rotation = originalRot * Quaternion.AngleAxis(90, Vector3.forward);
                    myVal = "90Hz";
                    break;
            }
            text.text = myVal;
            UnityEngine.Debug.LogError($"Frequency is: {myVal}", gameObject);
        }

        public void SetRandom()
        {
            index = Random.Range(0, 5);
            switch (index)
            {
                case 0:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-90, Vector3.forward);
                    myVal = "-90Hz";
                    break;
                case 1:
                    transform.rotation = originalRot * Quaternion.AngleAxis(-45, Vector3.forward);
                    myVal = "-45Hz";
                    break;
                case 2:
                    transform.rotation = originalRot * Quaternion.AngleAxis(0, Vector3.forward);
                    myVal = "0Hz";
                    break;
                case 3:
                    transform.rotation = originalRot * Quaternion.AngleAxis(45, Vector3.forward);
                    myVal = "45Hz";
                    break;
                case 4:
                    transform.rotation = originalRot * Quaternion.AngleAxis(90, Vector3.forward);
                    myVal = "90Hz";
                    break;
            }
            text.text = myVal;
            UnityEngine.Debug.LogError($"Frequency is: {myVal}", gameObject);
        }

        public string GetValue()
        {
            return myVal;
        }

        TextMeshProUGUI text;

        public void SetupText()
        {
            text = ObjectPool.instance.SpawnPoolObj(textObjPoolTag, textSpot.position, textSpot.rotation).GetComponent<TextMeshProUGUI>();
            text.text = myVal;
        }

        public void ShowInteractInstruction() { }

        public void HideInteractInstruction() { }

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
                eventName = "RadioTuningPiece";
                distributionOption = DistributionOption.serveOthers;
                myId = id;
                if (!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}