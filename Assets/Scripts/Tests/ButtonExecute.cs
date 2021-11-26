using UnityEngine;
using UnityEngine.Events;

public class ButtonExecute : MonoBehaviour
{    
    [SerializeField]KeyCode keyCode;
    [SerializeField] UnityEvent OnPress;
    void Update()
    {
        if(Input.GetKeyDown(keyCode)){
            OnPress.Invoke();
        }
    }
}
