using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.ChemicalRoom {
    public class PotionTransformSync : LocalNetTransformSync
    {
        private int portionId;

        private void Start() {
            if(DevBoy.yes)
                Destroy(this);
            
            Initilize("potionTransform", portionId);
            if(!DevBoy.yes)
                NetUnityEvents.instance.portionTransform.AddListener(RecieveData);
        }

        public override void SetID(int id) {
            portionId = id;
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.portionTransform.RemoveListener(RecieveData);
        }
    }
}