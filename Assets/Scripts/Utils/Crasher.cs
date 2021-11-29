using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crasher : MonoBehaviour
{
    bool crash = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            crash = true;
        }

        if (crash) CrashFunction();
    }

    void CrashFunction()
    {
        while (true)
        {
            CrashFunction();
        }
    }

}
