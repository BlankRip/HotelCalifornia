using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

public class testwin : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            NetGameManager.instance.ToggleWinScreen(true);
        if (Input.GetKeyDown(KeyCode.K))
            NetGameManager.instance.ToggleWinScreen(false);
    }
}
