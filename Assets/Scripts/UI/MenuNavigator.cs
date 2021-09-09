using System.Collections;
using System.Collections.Generic;
using Knotgames.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Knotgames.UI {
    public class MenuNavigator : MonoBehaviour, IMenuNavigator
    {
        [SerializeField] bool horizontalInputs;
        [SerializeField] List<GameObject> buttonsGameObjects;
        [SerializeField] MenuNavigator connectedNavigator;
        [SerializeField] ScriptableUIEvents uiEvents;
        private List<IMenuButton> buttons;
        private int currentIndex;

        private void Start() {
            buttons = new List<IMenuButton>();
            for (int i = 0; i < buttonsGameObjects.Count; i++) {
                buttons.Add(buttonsGameObjects[i].GetComponent<IMenuButton>());
                buttons[i].SetIndex(i);
            }
            currentIndex = 0;
            buttons[currentIndex].Selecte();
        }

        private void OnEnable() {
            if(buttons != null)
                buttons[currentIndex].Selecte();

            if(horizontalInputs) {
                uiEvents.onLeft.AddListener(CycleDown);
                uiEvents.onRight.AddListener(CycleUp);
            } else {
                uiEvents.onUp.AddListener(CycleDown);
                uiEvents.onDown.AddListener(CycleUp);
            }
            uiEvents.onEnter.AddListener(EnterPressed);
        }

        private void OnDisable() {
            if(horizontalInputs) {
                uiEvents.onLeft.RemoveListener(CycleDown);
                uiEvents.onRight.RemoveListener(CycleUp);
            } else {
                uiEvents.onUp.RemoveListener(CycleDown);
                uiEvents.onDown.RemoveListener(CycleUp);
            }
            uiEvents.onEnter.RemoveListener(EnterPressed);
        }

        private void CycleUp() {
            if(currentIndex != buttons.Count - 1)
                SelectButton(currentIndex + 1);
        }

        private void CycleDown() {
            if(currentIndex != 0)
                SelectButton(currentIndex - 1);
        }

        private void EnterPressed() {
            buttons[currentIndex].Click();
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