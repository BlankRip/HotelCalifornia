using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.UI
{
    public class PanelSwither: MonoBehaviour
    {
        Stack<GameObject> panels;
        [SerializeField] GameObject startPanel;

        private void Awake()
        {
            panels = new Stack<GameObject>();
            panels.Push(startPanel);
        }
        public void Switch(GameObject switchTo)
        {
            panels.Peek().SetActive(false);
            switchTo.SetActive(true);
            panels.Push(switchTo);
        }

        public void Pop()
        {
            if (panels.Count > 1)
            {
                panels.Peek().SetActive(false);
                panels.Pop();
                panels.Peek().SetActive(true);

            }
        }
    }
}