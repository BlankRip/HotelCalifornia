using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network.Debug
{
    public class NetDataShuttle : MonoBehaviour
    {

        [SerializeField] float pingDelay = 1;
        [SerializeField] [TextArea] string dataText;
        string pingValue;
        float nextUpdate = 0;

        void Start()
        {
            NetConnector.instance.OnMsgRecieveRaw += DataRecieve;
        }

        void Update()
        {
            if (NetConnector.instance.isConnected && Time.time > nextUpdate)
            {
                NetConnector.instance.SendDataToServer(
                    JsonUtility.ToJson(
                        new ShuttleData(dataText)
                    )
                );
                nextUpdate = Time.time + pingDelay;
            }
        }

        public void DataRecieve(string timeData)
        {
        }

        [System.Serializable]
        public class ShuttleData
        {

            public string eventName;
            public string distributionOption;
            public string data;

            public ShuttleData(string overloadData)
            {
                eventName = "dataShuttle";
                distributionOption = DistributionOption.serveMe;
                data = overloadData;
            }
        }

    }
}