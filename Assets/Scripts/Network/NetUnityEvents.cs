using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network {
    public class NetUnityEvents : MonoBehaviour
    {
        public static NetUnityEvents instance;
        public CustonEventString roomTiggerOnMsgRecieve;

        public CustonEventString portionTransform;
        public CustonEventString portionUseStatus;
        public CustonEventString puzzleSolvedEvent;
        public CustonEventString mixerEvents;

        public CustonEventString xoPieceEvent;

        public CustonEventString morseButtonEvent;
        public CustonEventString morsePlayerEvent;

        private void Awake() {
            if(instance == null)
                instance = this;
            else
                Destroy(this);
        }
    }
}