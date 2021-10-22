using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class EnableOnRoomFull : MonoBehaviour
    {
        [SerializeField] GameObject playButton;
        System.Action action;

        public void Start()
        {
            NetConnector.instance.OnMsgRecieve.AddListener(Enabler);
        }

        public void OnDestroy()
        {
            NetConnector.instance.OnMsgRecieve.RemoveListener(Enabler);
        }

        public void Enabler(string datastring)
        {
            string eventName = JsonUtility.FromJson<ExtractorClass>(datastring).eventName;
            switch (eventName)
            {
                case "roomFull":
                    playButton.SetActive(true);
                    break;
                case "youLeftRoom":
                    playButton.SetActive(false);
                    break;
            }
        }

        private class ExtractorClass
        {
            public string eventName;
        }
    }
}