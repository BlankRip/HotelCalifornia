using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class DItemTranformSync : LocalNetTransformSync
    {
        private int deliveryId;

        private void Start() {
            if(DevBoy.yes)
                Destroy(this);
            
            Initilize("deliveryTransform", deliveryId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.deliveryTransform.AddListener(RecieveData);
        }

        public override void SetID(int id) {
            deliveryId = id;
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.deliveryTransform.RemoveListener(RecieveData);
        }
    }
}
