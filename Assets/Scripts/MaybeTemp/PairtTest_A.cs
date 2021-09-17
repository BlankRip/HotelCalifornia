using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.LevelGen;

namespace Knotgames.Gameplay {
    public class PairtTest_A : MonoBehaviour, IPairPuzzle
    {
        [SerializeField] GameObject linkedObj;
        public void Link(GameObject obj, bool initiator) {
            PairtTest_A theOther = obj.GetComponent<PairtTest_A>();
            theOther.LinkingNow();

            if(initiator) {
                Debug.Log("Initiated So linking another");
                theOther.Link(this.gameObject, false);
            }
        }

        public void LinkingNow() {
            Debug.Log("Linking Now");
        }
    }
}