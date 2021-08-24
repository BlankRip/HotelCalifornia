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
        private IMenuNavigator menuNavigator;
        private int myIndex;

        private void Start() {
            menuNavigator = GetComponentInParent<IMenuNavigator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Ent");
            menuNavigator.SelectButton(myIndex);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void Click() {
            clickEvent.Invoke();
        }

        public void Deselect() {
            exitEvent.Invoke();
        }

        public void Selecte() {
            Debug.Log("Sel");
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
