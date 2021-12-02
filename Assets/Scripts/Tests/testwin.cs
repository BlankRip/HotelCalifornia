using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

public class testwin : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("humanwin");
            // NetGameManager.instance.ToggleWinScreen(true);
            Destroy(this);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("ghostwin");
            // NetGameManager.instance.ToggleWinScreen(false);
            Destroy(this);
        }
#endif
    }
}
