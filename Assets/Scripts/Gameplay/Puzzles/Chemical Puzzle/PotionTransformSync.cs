using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class PotionTransformSync : LocalNetTransformSync
    {
        private static int portionIds;
        public static void ResetIds() {
            portionIds = 0;
        }

        private int myId;

        private void Start() {
            if(DevBoy.yes)
                Destroy(this);
            
            myId = portionIds;
            portionIds++;
            Initilize("potionTransform", myId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.portionTransform.AddListener(RecieveData);
            SetDataSyncStatus(true);
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.portionTransform.RemoveListener(RecieveData);
        }
    }
}