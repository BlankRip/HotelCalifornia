using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunThisOnce : MonoBehaviour
{
    static List<string> objectNames = new List<string>();
    [SerializeField] string objectName;
    [SerializeField] UnityEvent OnFirstTime;
    [SerializeField] UnityEvent OnNextTime;

    private void Start()
    {
        if (objectName == null) objectName = gameObject.name;

        if (!objectNames.Contains(objectName))
        {
            OnFirstTime.Invoke();
            objectNames.Add(objectName);
            Debug.Log("First Time...");
        }
        else
        {
            OnNextTime.Invoke();
            Debug.Log("Already Done...");
        }
    }



}
