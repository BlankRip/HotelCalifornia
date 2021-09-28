using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTheDontKill : MonoBehaviour
{
    public static KillTheDontKill instance;
    public List<DontKillMe> toKill;

    private void Awake()
    {
        instance = this;
    }

    public void Kill()
    {
        Debug.LogError("KILLED!!!!!!");
        foreach(DontKillMe obj in toKill)
        {
            Destroy(obj.gameObject);
        }

        toKill.Clear();
    }
}
