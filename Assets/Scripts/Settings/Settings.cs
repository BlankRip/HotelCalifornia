
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

namespace Knotgames.GameSettings
{
    public class Settings : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public TMP_Dropdown resolutionDropdown;
        public Slider musicSlider;
        public Slider sfxSlider;
        float currentMusicVolume, currentSFXVolume;
        Resolution[] resolutions;

        void Start()
        {
            Screen.fullScreen = true;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            resolutions = Screen.resolutions;
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResolutionIndex = i;
            }
            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
            LoadSettings(currentResolutionIndex);
        }

        public void SetMusicVolume(float volume)
        {
            currentMusicVolume = volume;
            SaveSettings();
            float x = Mathf.Clamp(volume, -80f, -15f);
            audioMixer.SetFloat("MusicVolume", x);
        }

        public void SetSFXVolume(float volume)
        {
            currentSFXVolume = volume;
            SaveSettings();
            float x = Mathf.Clamp(volume, -80f, 0f);
            audioMixer.SetFloat("SFXVolume", x);
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            SaveSettings();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            SaveSettings();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
            PlayerPrefs.SetString("FullscreenPreference", Screen.fullScreen.ToString());
            PlayerPrefs.SetFloat("MusicVolumePreference", currentMusicVolume);
            PlayerPrefs.SetFloat("SFXVolumePreference", currentSFXVolume);
        }

        private void LoadSettings(int currentResolutionIndex)
        {
            if (PlayerPrefs.HasKey("ResolutionPreference"))
                resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            else
                resolutionDropdown.value = currentResolutionIndex;
            if (PlayerPrefs.HasKey("FullscreenPreference"))
                Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetString("FullscreenPreference"));
            else
                Screen.fullScreen = true;
            if (PlayerPrefs.HasKey("MusicVolumePreference"))
            {
                musicSlider.value = PlayerPrefs.GetFloat("MusicVolumePreference");
                SetMusicVolume(PlayerPrefs.GetFloat("MusicVolumePreference"));
            }
            else
            {
                musicSlider.value = -15f;
                SetMusicVolume(-15f);
            }
            if (PlayerPrefs.HasKey("SFXVolumePreference"))
            {
                sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumePreference");
                SetSFXVolume(PlayerPrefs.GetFloat("SFXVolumePreference"));
            }
            else
            {
                sfxSlider.value = 0;
                SetSFXVolume(0);
            }

            Debug.LogError($"<color=green>Loaded Settings: {resolutionDropdown.value}, {Screen.fullScreen}, {musicSlider.value}, {sfxSlider.value}</color>");
        }

        public void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}