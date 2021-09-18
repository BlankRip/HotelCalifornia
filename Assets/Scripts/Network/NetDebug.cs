using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetDebug : MonoBehaviour
{

    public static Queue<System.Action> DebugActions = new Queue<System.Action>();

    void Update()
    {
        if (DebugActions.Count > 0) DebugActions.Dequeue().Invoke();
    }
}
