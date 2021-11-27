
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedCaller : MonoBehaviour
{
    [SerializeField] UnityEvent OnCall;

    public void Call(float delay)
    {
        StartCoroutine(Run(delay));
    }

    IEnumerator Run(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnCall.Invoke();
    }
}
