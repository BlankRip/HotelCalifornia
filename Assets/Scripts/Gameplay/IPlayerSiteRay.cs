using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface IPlayerSiteRay
    {
        bool InSite();
        GameObject PlayerInSiteObj();
    }
}