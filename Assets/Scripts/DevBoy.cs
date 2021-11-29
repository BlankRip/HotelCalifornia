using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DevBoy
{
    public static bool yes = false;

    static DevBoy()
    {
        if(yes)
            KnotRandom.theRand = new System.Random(-1);
    }
}