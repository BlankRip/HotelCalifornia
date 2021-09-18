using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerInSite
    {
        bool InLineOfSite();
        GameObject GetPlayerObject();
    }
}