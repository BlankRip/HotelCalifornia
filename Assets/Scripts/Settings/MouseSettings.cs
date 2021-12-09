using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.GameSettings
{
    public class MouseSettings : MonoBehaviour
    {
        public Slider senseSlider;
        public TextMeshProUGUI senseText;
        float currentSense;
        Resolution[] resolutions;
        [SerializeField] SOFloat senseFloat;

        void Start()
        {
            LoadSettings();
        }

        public void SetMouseSensitivity(float sense)
        {
            if (sense > 9)
                sense = 9;
            if (sense < 0.5f)
                sense = 0.5f;

            senseFloat.value = sense;
            currentSense = sense;
            senseText.text = sense.ToString("F2");
            SaveSettings();
        }

        public void SetMouseSensitivity(string sense)
        {
            if (sense != "")
            {
                float x = (float)double.Parse(sense);
                if (x > 9)
                    x = 9;
                if (x < 0.5f)
                    x = 0.5f;

                senseFloat.value = x;
                currentSense = x;
                senseSlider.value = x;
                senseText.text = x.ToString("F2");
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("Sensitivity", currentSense);
        }

        private void LoadSettings()
        {
            if (PlayerPrefs.HasKey("Sensitivity"))
            { senseSlider.value = PlayerPrefs.GetFloat("Sensitivity"); senseText.text = senseSlider.value.ToString("F2"); senseFloat.value = senseSlider.value; }
            else
            { senseSlider.value = 3; senseText.text = senseSlider.value.ToString("F2"); senseFloat.value = senseSlider.value; }

            Debug.LogError($"LOADED MOUSE SETTINGS : {PlayerPrefs.GetFloat("Sensitivity")}");
        }
    }
}