using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAfter : MonoBehaviour
{
    
    [SerializeField] float startDelayTime;
    [SerializeField] UnityEngine.Events.UnityEvent OnWaitOver;
    void Start()
    {
       Invoke("StartAfterTime", startDelayTime);
    }

    void StartAfterTime()
    {
        OnWaitOver.Invoke();
    }

}
