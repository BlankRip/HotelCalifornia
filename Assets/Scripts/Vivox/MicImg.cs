using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicImg : MonoBehaviour
{
    private void Start() {
        InGameVivox inGameVivox = FindObjectOfType<InGameVivox>();
        if(inGameVivox != null)
            inGameVivox.SetMicIndicator(GetComponent<Image>());
    }
}
