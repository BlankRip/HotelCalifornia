using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.LevelGen {
    public class PairtTest_A : MonoBehaviour, IPairPuzzleSetup
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