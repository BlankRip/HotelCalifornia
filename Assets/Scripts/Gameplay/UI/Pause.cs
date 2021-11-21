using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.UI;

namespace Knotgames.Gameplay.UI {
    public class Pause : MonoBehaviour
    {
        private enum PanelState {Nada, Pause, Controls, Settings}
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject controlsPanel;
        [SerializeField] GameObject settingsPanel;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] List<AbilityDetailsUi> detailsList;
        private PanelState state;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(pausePanel.activeSelf == false)
                    ShowPause();
                else if(state != PanelState.Pause) {
                    switch (state) {
                        case PanelState.Controls:
                            ToggleControlsScreen(false);
                            break;
                        case PanelState.Settings:
                            ToggleSettingsScreen(false);
                            break;
                    }
                } else if(pausePanel.activeSelf == true)
                    HidePause();
            }
        }

        public void ShowPause() {
            SetUpDetails();
            state = PanelState.Pause;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
        }

        private void SetUpDetails() {
            for (int i = 0; i < characterData.abilityTypes.Count; i++) {
                if(characterData.abilityTypes[i] != AbilityType.Nada) {
                    detailsList[i].SetData(abilityUiCollection.GetAbilityData(characterData.abilityTypes[i]));
                    detailsList[i].gameObject.SetActive(true);
                } else
                    detailsList[i].gameObject.SetActive(false);
            }
        }

        public void HidePause() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            state = PanelState.Nada;
            pausePanel.SetActive(false);
        }

        public void ToggleControlsScreen(bool activeState) {
            ToggeleScreen(activeState, controlsPanel, PanelState.Controls);
        }

        public void ToggleSettingsScreen(bool activeState) {
            ToggeleScreen(activeState, settingsPanel, PanelState.Settings);
        }

        private void ToggeleScreen(bool activeState, GameObject panle, PanelState pState) {
            panle.SetActive(activeState);
            if(activeState)
                state = pState;
            else
                state = PanelState.Pause;
        }
    }
}