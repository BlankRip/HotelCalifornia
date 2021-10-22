using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class EnableOnRoomFull : MonoBehaviour
    {
        [SerializeField] GameObject playButton;
        private void Awake()
        {
            NetConnector.instance.OnMsgRecieve.AddListener(Hear);
        }
        private void OnDestroy()
        {
            NetConnector.instance.OnMsgRecieve.RemoveListener(Hear);
        }

        public void Hear(string datastring)
        {
            switch (datastring)
            {
                case "roomFull":
                    playButton.SetActive(true);
                    break;
                case "youLeftRoom":
                    playButton.SetActive(false);
                    break;
            }
        }
    }
}