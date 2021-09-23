using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetDebug : MonoBehaviour
{
    public static NetDebug instance;
    public bool isTesting;
    public Queue<string> DebugActions = new Queue<string>();

    void Awake() {
        instance = this;
    }

    void Update()
    {
        if (DebugActions.Count > 0) Debug.Log(DebugActions.Dequeue());
    }

    public void AddDebug(string displayString)
    {
        if (isTesting) DebugActions.Enqueue(displayString);
    }

}
