using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Knotgames.UI {
    public class MenuButton : MonoBehaviour, IMenuButton
    {
        [SerializeField] UnityEvent onClick;
        private IMenuNavigator menuNavigator;
        private int myIndex;

        private void Start() {
            menuNavigator = GetComponentInParent<IMenuNavigator>();
        }

        public void Click() {
            onClick.Invoke();
        }

        public void Deselect() {
            throw new System.NotImplementedException();
        }

        public void Selecte() {
            throw new System.NotImplementedException();
        }

        public void SetIndex(int index) {
            myIndex = index;
        }

        public void SetOnClick(UnityAction call) {
            onClick.AddListener(call);
        }
    }
}