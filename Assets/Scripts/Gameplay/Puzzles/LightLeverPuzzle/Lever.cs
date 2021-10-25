using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class Lever : MonoBehaviour, IInteractable
    {
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] string textPoolTag;
        [SerializeField] Transform textPos;
        private TextMeshProUGUI myText;
        private LightColour originalColor;
        private LightColour myColour;
        private List<ILight> myLights;

        private void Start() {
            myColour = lightLever.manager.GetAvailableLeverColor();
            originalColor = myColour;
            myText = ObjectPool.instance.SpawnPoolObj(textPoolTag, textPos.position, textPos.rotation).GetComponent<TextMeshProUGUI>();
            myText.text = myColour.ToString();
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

        public void HideInteractInstruction() {

        }

        public void ShowInteractInstruction() {

        }
    }
}