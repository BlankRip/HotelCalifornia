using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI {
    public class PanelSwither: MonoBehaviour
    {
        Stack<GameObject> panels;
        [SerializeField] GameObject startPanel;
        [SerializeField] PanelSwither parentSwitcher;
        [SerializeField] int minPanels = 1;
        private bool active;

        private void Awake() {
            panels = new Stack<GameObject>();
            panels.Push(startPanel);
        }

        private void OnEnable() {
            if(parentSwitcher != null)
                parentSwitcher.SwitchActiveState(false);
            active = true;

            if(panels.Count == 0)
                panels.Push(startPanel);
        }

        public void SwitchActiveState(bool state) {
            active = state;
        }

        private void Update() {
            if(active && Input.GetKeyDown(KeyCode.Escape))
                Pop();
        }

        public void Switch(GameObject switchTo) {
            panels.Peek().SetActive(false);
            switchTo.SetActive(true);
            panels.Push(switchTo);
        }

        public void Pop() {
            if (panels.Count > minPanels) {
                panels.Peek().SetActive(false);
                if(panels.Count != 0) {
                    panels.Pop();
                    panels.Peek().SetActive(true);
                }
            } else if(parentSwitcher != null) {
                parentSwitcher.SwitchActiveState(true);
                parentSwitcher.Pop();
            }
        }
    }
}