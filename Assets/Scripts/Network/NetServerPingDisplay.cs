using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

namespace Knotgames.Network
{
    public class NetServerPingDisplay : MonoBehaviour
    {
        [SerializeField] Text logText;
        [SerializeField] float pingDelay = 1;
        Stopwatch clock = new Stopwatch();
        string pingValue;
        float nextUpdate = 0;

        void Start()
        {
            clock.Start();
            NetConnector.instance.OnMsgRecieveRaw += CalculatePing;
        }

        void OnDestroy()
        {
            clock.Stop();
        }

        void Update()
        {
            if (NetConnector.instance.isConnected && Time.time > nextUpdate)
            {
                NetConnector.instance.SendDataToServer(
                    JsonUtility.ToJson(
                        new PingSend(clock.ElapsedMilliseconds)
                    )
                );
                nextUpdate = Time.time + pingDelay;
            }

            logText.text = $"[Ping] : {pingValue}ms";

        }

        public void CalculatePing(string timeData)
        {   
            PingSend data = JsonUtility.FromJson<PingSend>(timeData);
            pingValue = (clock.ElapsedMilliseconds - data.time).ToString();
        }

        [System.Serializable]
        public class PingSend
        {

            public string eventName;
            public string distributionOption;
            public float time;

            public PingSend(long time)
            {
                eventName = "ping";
                distributionOption = DistributionOption.serveMe;
                this.time = time;
            }

        }

    }
}


