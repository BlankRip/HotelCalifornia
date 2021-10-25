using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.LevelLight {
    public class Light : MonoBehaviour, ILight
    {
        private ILightLeverManager manager;
        [SerializeField] ScriptableLightMatDataBase matDataBase;
        [SerializeField] GameObject myLight;
        [SerializeField] float lightsOnTime = 5;
        private LightColour myColour;
        private Renderer myLightRenderer;
        private float timer;
        private bool timerOn;


        private void Start() {
            myColour = manager.GetAvailableLightColor(this);
            myLightRenderer = myLight.GetComponent<Renderer>();
            myLightRenderer.material = matDataBase.GetMaterial(myColour);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= lightsOnTime)
                    timerOn = false;
            }
        }

        public void ActivateLight() {
            timer = 0;
            timerOn = true;
            myLight.SetActive(true);
        }
    }
}