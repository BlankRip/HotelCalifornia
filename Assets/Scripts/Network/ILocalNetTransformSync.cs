using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network {
    public interface ILocalNetTransformSync
    {
        void SetDataSyncStatus(bool sync);
    }
}