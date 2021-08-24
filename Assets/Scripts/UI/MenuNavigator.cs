using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI {
    public class MenuNavigator : MonoBehaviour, IMenuNavigator
    {
        [SerializeField] List<GameObject> buttonsGameObjects;
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
            buttons[currentIndex].Selecte();
        }

        private void Update() {
            //TODO: CHANGE TO NEW INPUT SYSTEM
            if(Input.GetKeyDown(KeyCode.DownArrow))
                CycleDown();
            else if(Input.GetKeyDown(KeyCode.UpArrow))
                CycleUp();
            else if(Input.GetKeyDown(KeyCode.Return))
                buttons[currentIndex].Click();
        }

        private void CycleUp() {
            if(currentIndex != 0)
                SelectButton(currentIndex - 1);
        }

        private void CycleDown() {
            if(currentIndex != buttons.Count - 1)
                SelectButton(currentIndex + 1);
        }

        public void SelectButton(int index) {
            if(currentIndex != index) {
                buttons[currentIndex].Deselect();
                currentIndex = index;
                buttons[currentIndex].Selecte();
            }
        }
    }
}