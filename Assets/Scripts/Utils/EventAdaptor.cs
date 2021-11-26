using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventAdaptor : MonoBehaviour
{
    [SerializeField] EventElement[] eventArray;

    public void CallEvents(string eventName)
    {
        for (int i = 0; i < eventArray.Length; i++)
        {
            if(eventName == eventArray[i].name)
            {
                eventArray[i].Event.Invoke();
                break;
            }
        }
    }

}

[System.Serializable]
public struct EventElement
{
    public string name;
    public UnityEvent Event;
}