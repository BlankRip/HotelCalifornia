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
        float pingValue;
        float nextUpdate = 0;
        Ping pingData;

        void Start()
        {
            clock.Start();
            NetConnector.instance.OnMsgRecieveRaw += CalculatePing;
            pingData = new Ping("https://hotelcaliforniagame.herokuapp.com/");
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
            switch(data.eventName)
            {
                case "ping":
                if (data.time > 0)
                    pingValue = clock.ElapsedMilliseconds - data.time;
                break;
            }
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