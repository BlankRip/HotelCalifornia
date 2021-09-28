using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Knotgames.Network
{

    public class NetConnector : MonoBehaviour
    {

        public static NetConnector instance;
        public static bool running;

        #region Public Variables
        public bool localTesting;
        public string ipLocal;
        public string ipAddressString;
        public int portNumber;
        public System.Action<string> OnMsgRecieveRaw;
        public CustonEventString OnMsgRecieve;
        public SOBool isConnected;
        public SOString playerID;

        #endregion

        const int reConnectionWaitTime = 3000;

        WebSocket ws;
        INetEventHub eventHub;

        Queue<System.Action> recievedEvents = new Queue<System.Action>();

        void Awake()
        {
            if (NetConnector.instance == null) NetConnector.instance = this;
            else Destroy(this);
            running = true;
        }

        void Start()
        {
            if (NetConnector.instance.ws == null)
            {
                isConnected.value = false;
                playerID.value = null;
                eventHub = new NetEventHub(isConnected, playerID);
                ConnectToServer();
            }
        }

        void Update()
        {
            while (recievedEvents.Count > 0) recievedEvents.Dequeue().Invoke();
        }

        async void ConnectToServer()
        {
            while (!isConnected.value && running)
            {
                UnityEngine.Debug.Log("Connection attempt...");
                string connectionURL = !localTesting ? ipAddressString : $"{ipLocal}:{portNumber}";
                ws = new WebSocket($"ws://{connectionURL}");
                ws.OnMessage += (sender, e) => DataReciver(e);
                ws.Connect();
                await Task.Delay(reConnectionWaitTime);
            }
        }

        void DataReciver(MessageEventArgs eventData)
        {
            string val = Encoding.UTF8.GetString(eventData.RawData);
            OnMsgRecieveRaw.Invoke(val);


            recievedEvents.Enqueue(
            () =>
                {
                    eventHub.Listen(val);
                    OnMsgRecieve.Invoke(val);
                }
            );


        }

        void OnDestroy()
        {
            ws?.Close(CloseStatusCode.Normal, JsonUtility.ToJson(new PlayerData() { playerID = playerID.value }));
            running = false;
        }

        public void SendDataToServer(string data)
        {
            ws.Send(data);
        }

        [System.Serializable]
        public class PlayerData
        {
            public string playerID;
        }

    }

    [System.Serializable]
    public class CustonEventString : UnityEvent<string> { }

}