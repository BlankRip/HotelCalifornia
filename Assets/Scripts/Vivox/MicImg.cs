using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicImg : MonoBehaviour
{
    private void Awake() {
        InGameVivox inGameVivox = FindObjectOfType<InGameVivox>();
        inGameVivox.SetMicIndicator(GetComponent<Image>());
    }
}
