using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LeverLight {
    public class Light : MonoBehaviour, ILight
    {
        [SerializeField] ScriptableLightLeverManager lightLever;
        [SerializeField] ScriptableLightMatDataBase matDataBase;
        [SerializeField] GameplayEventCollection gamplayEvents;
        [SerializeField] GameObject myLight;
        [SerializeField] float lightsOnTime = 5;
        private LightColour myColour;
        private Renderer myLightRenderer;
        private float timer;
        private bool timerOn;
        private int lastIndex;
        private int twistIndex;


        private void Start() {
            myColour = lightLever.manager.GetAvailableLightColor(this);
            myLightRenderer = myLight.GetComponent<Renderer>();
            myLightRenderer.material = matDataBase.GetMaterial(myColour);
            SetUpDilusionStuff();
        }

        private void SetUpDilusionStuff() {
            lastIndex = System.Enum.GetValues(typeof(LightColour)).Length - 1;
            twistIndex = (int)myColour;
            gamplayEvents.twistVision.AddListener(TwistVision);
            gamplayEvents.fixVision.AddListener(FixVision);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= lightsOnTime) {
                    timerOn = false;
                    myLight.SetActive(false);
                }
            }
        }

        private void OnDestroy() {
            gamplayEvents.twistVision.RemoveListener(TwistVision);
            gamplayEvents.fixVision.RemoveListener(FixVision);
        }

        private void TwistVision() {
            CycleTwistedColor();
            if(twistIndex == (int)myColour)
                CycleTwistedColor();
            myLightRenderer.material = matDataBase.GetMaterial((LightColour)twistIndex);
        }

        private void CycleTwistedColor() {
            if(twistIndex == lastIndex)
                twistIndex = 0;
            else 
                twistIndex++;
        }

        private void FixVision() {
            myLightRenderer.material = matDataBase.GetMaterial(myColour);
        }

        public void ActivateLight() {
            timer = 0;
            timerOn = true;
            myLight.SetActive(true);
        }
    }
}