using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI {
    public class MenuNavigator : MonoBehaviour, IMenuNavigator
    {
        [SerializeField] bool horizontalInputs;
        [SerializeField] bool secondNavigator;
        [SerializeField] List<GameObject> buttonsGameObjects;
        [SerializeField] MenuNavigator connectedNavigator;
        private List<IMenuButton> buttons;
        private int currentIndex;

        private KeyCode positiveCode;
        private KeyCode negetiveCode;

        private void Start() {
            buttons = new List<IMenuButton>();
            for (int i = 0; i < buttonsGameObjects.Count; i++) {
                if(buttonsGameObjects[i] != null) {
                    buttons.Add(buttonsGameObjects[i].GetComponent<IMenuButton>());
                    if(!secondNavigator)
                        buttons[i].SetIndex(i);
                } else 
                    return;
            }
            currentIndex = 0;
            buttons[currentIndex].Selecte();

            if(horizontalInputs) {
                negetiveCode = KeyCode.LeftArrow;
                positiveCode = KeyCode.RightArrow;
            } else {
                negetiveCode = KeyCode.UpArrow;
                positiveCode = KeyCode.DownArrow;
            }
        }

        private void OnEnable() {
            if(buttons != null)
                buttons[currentIndex].Selecte();
        }

        private void Update() {
            if(Input.GetKeyDown(negetiveCode))
                CycleDown();
            else if(Input.GetKeyDown(positiveCode))
                CycleUp();
            else if(Input.GetKeyDown(KeyCode.Return))
                buttons[currentIndex].Click();
        }

        private void CycleUp() {
            if(currentIndex != buttons.Count - 1)
                SelectButton(currentIndex + 1);
        }

        private void CycleDown() {
            if(currentIndex != 0)
                SelectButton(currentIndex - 1);
        }

        public void SelectButton(int index) {
            if(currentIndex != index) {
                buttons[currentIndex].Deselect();
                currentIndex = index;
                buttons[currentIndex].Selecte();
                if(connectedNavigator != null)
                    connectedNavigator.SelectButton(buttons[currentIndex]);
            }
        }

        public void SelectButton(IMenuButton button) {
            int index = buttons.IndexOf(button);
            if(index != currentIndex) {
                buttons[currentIndex].Deselect();
                currentIndex = index;
                buttons[currentIndex].Selecte();
            }
        }

        public void Click()
        {
            buttons[currentIndex].Click();
        }
    }
}