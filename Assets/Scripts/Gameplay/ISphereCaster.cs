using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public interface ISphereCaster
    {
        List<GameObject> GetOppositPlayersInSphere(float radius);
        List<GameObject> GetFriendlyPlayersInSphere(float radius);
    }
}