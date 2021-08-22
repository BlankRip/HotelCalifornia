using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEvent clickEvent;
    [SerializeField] UnityEvent enterEvent;
    [SerializeField] UnityEvent exitEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        clickEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        enterEvent.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        exitEvent.Invoke();
    }
}
