using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Knotgames.UI {
    public class PanelSwitcher : MonoBehaviour
    {
        [SerializeField] Button button;
        [SerializeField] GameObject from;
        [SerializeField] GameObject to;
        [SerializeField] UnityEvent onSwitchEvent;

        private void Start() {
            if(button != null)
                button.onClick.AddListener(() => Switch());
        }

        public void Switch() {
            to.SetActive(true);
            from.SetActive(false);
            onSwitchEvent?.Invoke();
        }
    }
}
