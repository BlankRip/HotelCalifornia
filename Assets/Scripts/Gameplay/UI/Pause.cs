using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using Knotgames.UI;

namespace Knotgames.Gameplay.UI {
    public class Pause : MonoBehaviour
    {
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject controlsPanel;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] ScriptableAbilityUiCollection abilityUiCollection;
        [SerializeField] List<AbilityDetailsUi> detailsList;
        private bool showing;

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(pausePanel.activeSelf == false)
                    ShowPause();
                else if(showing) {
                    if(pausePanel.activeSelf == true)
                        HidePause();
                } else if (controlsPanel.activeSelf == true) {
                    controlsPanel.SetActive(false);
                    showing = true;
                }
            }
        }

        public void ShowPause() {
            SetUpDetails();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            showing = true;
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
            showing = false;
            pausePanel.SetActive(false);
        }

        public void ToggleControlsScreen(bool activateState) {
            controlsPanel.SetActive(activateState);
            showing = !activateState;
        }
    }
}