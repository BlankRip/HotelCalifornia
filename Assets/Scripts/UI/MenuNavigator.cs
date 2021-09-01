using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI {
    public class MenuNavigator : MonoBehaviour, IMenuNavigator
    {
        [SerializeField] bool horizontalInputs;
        [SerializeField] List<GameObject> buttonsGameObjects;
        private List<IMenuButton> buttons;
        private int currentIndex;

        //TODO Testing with old system
        private KeyCode positiveCode;
        private KeyCode negetiveCode;

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

            //TODO Change to new input system
            if(horizontalInputs) {
                negetiveCode = KeyCode.LeftArrow;
                positiveCode = KeyCode.RightArrow;
            } else {
                negetiveCode = KeyCode.DownArrow;
                positiveCode = KeyCode.UpArrow;
            }
        }

        private void OnDisable() {
            //TODO reset the events to null
            if(horizontalInputs) {

            } else {

            }
        }

        private void Update() {
            //TODO: CHANGE TO NEW INPUT SYSTEM
            if(Input.GetKeyDown(negetiveCode))
                CycleDown();
            else if(Input.GetKeyDown(positiveCode))
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