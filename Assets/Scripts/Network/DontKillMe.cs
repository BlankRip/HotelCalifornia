using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontKillMe : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}