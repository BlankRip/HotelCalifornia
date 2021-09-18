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
            LinkingNow(obj);

            if(initiator) {
                Debug.Log("Initiated So linking another on");
                theOther.Link(this.gameObject, false);
            }
        }

        public void LinkingNow(GameObject obj) {
            linkedObj = obj;
            Debug.Log("Linking To");
        }
    }
}