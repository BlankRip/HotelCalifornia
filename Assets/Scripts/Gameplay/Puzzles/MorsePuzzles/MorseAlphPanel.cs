using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    public class MorseAlphPanel : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] GameObject buttonObj;
        [SerializeField] Transform buttonParent;
        private MorseButton currentButton;
        List<char> buttonValues;

        private void Start() {
            ButtonsSetUp();
        }

        private void ButtonsSetUp() {
            buttonValues = new List<char>{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 
                'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            foreach (char str in buttonValues) {
                Button button = GameObject.Instantiate(buttonObj, buttonParent).GetComponent<Button>();
                button.onClick.AddListener(() => SelectChar(str));
                button.GetComponentInChildren<TextMeshProUGUI>().text = str.ToString();
            }
            Destroy(buttonObj);
        }

        public void OpenPanel(MorseButton button) {
            if(currentButton == null) {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                currentButton = button;
                panel.SetActive(true);
            }
        }

        private void SelectChar(char c) {
            currentButton.SetText(c);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            currentButton = null;
            panel.SetActive(false);
            AudioPlayer.instance.PlayAudio2DOneShot(ClipName.Numpad);
        }
    }
}