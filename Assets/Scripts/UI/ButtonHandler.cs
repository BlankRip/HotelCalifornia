using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Knotgames.UI {
    public class ButtonHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IMenuButton
    {
        [SerializeField] UnityEvent clickEvent;
        [SerializeField] UnityEvent enterEvent;
        [SerializeField] UnityEvent exitEvent;
        [SerializeField] GameObject navigatorObject;
        private IMenuNavigator menuNavigator;
        private int myIndex;

        private void Start() {
            if(menuNavigator == null)
                menuNavigator = GetComponentInParent<IMenuNavigator>();
            else
                menuNavigator = navigatorObject.GetComponent<IMenuNavigator>();
        }

        public void OnPointerClick(PointerEventData eventData) {
            Click();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if(menuNavigator!=null)
                menuNavigator.SelectButton(myIndex);
        }

        public void OnPointerExit(PointerEventData eventData) {
            
        }

        public void Click() {
            clickEvent.Invoke();
        }

        public void Deselect() {
            exitEvent.Invoke();
        }

        public void Selecte() {
            enterEvent.Invoke();
        }

        public void SetIndex(int index) {
            myIndex = index;
        }

        public void SetOnClick(UnityAction call) {
            clickEvent.AddListener(call);
        }
    }
}
