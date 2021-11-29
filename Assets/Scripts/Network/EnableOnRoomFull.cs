using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{
    public class EnableOnRoomFull : MonoBehaviour
    {
        [SerializeField] GameObject[] playButtons;

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
                    foreach (GameObject playButton in playButtons)
                        playButton.SetActive(true);
                    break;
                case "youLeftRoom":
                    foreach (GameObject playButton in playButtons)
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