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
        private LightColor myColour;
        private Renderer myLightRenderer;
        private float timer;
        private bool timerOn;
        private int lastIndex;
        private int twistIndex;
        GameObject myEffect;


        private void Start() {
            myColour = lightLever.manager.GetAvailableLightColor(this);
            myLightRenderer = myLight.GetComponent<Renderer>();
            myEffect = GameObject.Instantiate(matDataBase.GetMaterial(myColour), myLight.transform.position, Quaternion.identity);
            myEffect.transform.SetParent(transform);
            myEffect.SetActive(false);
            SetUpDilusionStuff();
        }

        private void SetUpDilusionStuff() {
            lastIndex = System.Enum.GetValues(typeof(LightColor)).Length - 1;
            twistIndex = (int)myColour;
            gamplayEvents.twistVision.AddListener(TwistVision);
            gamplayEvents.fixVision.AddListener(FixVision);
        }

        private void Update() {
            if(timerOn) {
                timer += Time.deltaTime;
                if(timer >= lightsOnTime) {
                    timerOn = false;
                    myEffect.SetActive(false);
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
            Destroy(myEffect);
            myEffect = GameObject.Instantiate(matDataBase.GetMaterial((LightColor)twistIndex), myLight.transform.position, Quaternion.identity, transform);
            myEffect.SetActive(false);
        }

        private void CycleTwistedColor() {
            if(twistIndex == lastIndex)
                twistIndex = 0;
            else 
                twistIndex++;
        }

        private void FixVision() {
            Destroy(myEffect);
            myEffect = GameObject.Instantiate(matDataBase.GetMaterial(myColour), myLight.transform.position, Quaternion.identity, transform);
            myEffect.SetActive(false);
        }

        public void ActivateLight() {
            timer = 0;
            timerOn = true;
            myEffect.SetActive(true);
        }
    }
}