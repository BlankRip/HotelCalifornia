using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.Abilities;
using Knotgames.Network;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class Lever : MonoBehaviour, IInteractable, IInterfear
    {
        private static int leverId;
        public static void ResetID() {
            leverId = 0;
        }

        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        private TextMeshProUGUI myText;
        private LightColor originalColor;
        private LightColor myColor;
        private List<ILight> myLights;
        private bool timerOn;
        private float interfereTime = 10;
        private float timer;
        private List<LightColor> interfereColors;
        private int colorIndex;
        private Animator animator;

        private int myId;
        private DataToSend pulledData;
        private DataToSend interfereData;

        private void Start() {
            myColor = lightLever.manager.GetAvailableLeverColor();
            originalColor = myColor;
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myColor.ToString();
            animator = GetComponent<Animator>();

            interfereColors = new List<LightColor>(lightLever.manager.GetAllAvailableColors());
            interfereColors.Remove(originalColor);

            myId = leverId;
            leverId++;
            pulledData = new DataToSend("leverPull", myId);
            interfereData = new DataToSend("leverInterfere", myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.lightLeverEvents.AddListener(RecieveData);
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.lightLeverEvents.RemoveListener(RecieveData);
        }

        private void SendInterfearData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(interfereData));
        }

        private void SendPulledData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(pulledData));
        }

        private void RecieveData(string recieved) {
            ExtractionClass extracted = JsonUtility.FromJson<ExtractionClass>(recieved);
            if(extracted.myId == myId) {
                if(extracted.eventName == "leverPull")
                    ActivateLights();
                else if(extracted.eventName == "leverInterfere") 
                    StartInterfere();
            }
        }

        public void Interact() {
            ActivateLights();
            if(!DevBoy.yes)
                SendPulledData();
        }
        bool canTrigger = true;
        private void ActivateLights() {
            if(canTrigger)
            {
                canTrigger = false;
                Invoke("ResetCanTrigger", 5);
                animator.SetTrigger("open");
            }
            if(myLights == null)
                myLights = lightLever.manager.GetLightsOfColor(myColor);
            foreach(ILight light in myLights)
                light.ActivateLight();
        }

        void ResetCanTrigger()
        {
            canTrigger = true;
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= interfereTime) {
                    timerOn = false;
                    UpdateLeverColor(originalColor);
                }
            }
        }

        public void HideInteractInstruction() {
            InstructionText.instance.HideInstruction();
        }

        public void ShowInteractInstruction() {
            InstructionText.instance.ShowInstruction("Press \'LMB\' To Interact");
        }

        public bool CanInterfear() {
            return !timerOn;
        }

        public void Interfear() {
            StartInterfere();
            if(!DevBoy.yes)
                SendInterfearData();
        }

        private void StartInterfere() {
            timer = 0;
            timerOn = true;
            if(colorIndex == interfereColors.Count - 1)
                colorIndex = 0;
            else
                colorIndex++;
            UpdateLeverColor(interfereColors[colorIndex]);
        }

        private void UpdateLeverColor(LightColor color) {
            myColor = color;
            myText.text = myColor.ToString();
            myLights = null;
        }

        private class ExtractionClass {
            public int myId;
            public string eventName;
        }

        private class DataToSend {
            public int myId;
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend(string eventName, int id) {
                myId = id;
                this.eventName = eventName;
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}