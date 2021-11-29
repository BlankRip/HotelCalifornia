using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DevBoy
{
    public static bool yes = true;

    static DevBoy()
    {
        if(yes)
            KnotRandom.theRand = new System.Random(-1);
    }
}