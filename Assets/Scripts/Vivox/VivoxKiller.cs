using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VivoxKiller : MonoBehaviour
{
    private void Awake() {
        if(VoIPManager.instance != null)
            Destroy(VoIPManager.instance.gameObject);
        Destroy(this.gameObject);
    }
}
