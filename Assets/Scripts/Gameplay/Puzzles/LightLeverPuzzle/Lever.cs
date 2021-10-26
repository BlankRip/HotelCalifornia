using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Gameplay.Abilities;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class Lever : MonoBehaviour, IInteractable, IInterfear
    {
        //TODO Remove testing bool
        [SerializeField] bool testing;
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        private TextMeshProUGUI myText;
        private LightColour originalColor;
        private LightColour myColour;
        private List<ILight> myLights;
        private bool timerOn;
        private float interfereTime = 10;
        private float timer;
        private List<LightColour> interfereColors;
        private int colorIndex;

        private void Start() {
            myColour = lightLever.manager.GetAvailableLeverColor();
            originalColor = myColour;
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myColour.ToString();

            interfereColors = new List<LightColour>(lightLever.manager.GetAllAvailableColors());
            interfereColors.Remove(originalColor);
        }

        public void Interact() {
            ActivateLights();
        }

        private void ActivateLights() {
            if(myLights == null)
                myLights = lightLever.manager.GetLightsOfClour(myColour);
            foreach(ILight light in myLights)
                light.ActivateLight();
        }

        private void Update() {
            //Todo removie testing part
            if(Input.GetKeyDown(KeyCode.U) && testing)
                Interact();
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= interfereTime) {
                    timerOn = false;
                    UpdateLeverColor(originalColor);
                }
            }
        }

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {

        }

        public bool CanInterfear() {
            return !timerOn;
        }

        public void Interfear() {
            StartColorSwitch();
        }

        private void StartColorSwitch() {
            timer = 0;
            timerOn = true;
            if(colorIndex == interfereColors.Count - 1)
                colorIndex = 0;
            else
                colorIndex++;
            UpdateLeverColor(interfereColors[colorIndex]);
        }

        private void UpdateLeverColor(LightColour color) {
            myColour = color;
            myText.text = myColour.ToString();
            myLights = null;
        }
    }
}