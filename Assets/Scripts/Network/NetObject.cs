using UnityEngine;

namespace Knotgames.Network
{

    public class NetObject : MonoBehaviour, INetObject
    {
        public string id;
        public string ownerID;
        public bool isOwner;

        public delegate void MessageEvent(string dataString);
        public MessageEvent OnMessageRecieve;

        public bool IsMine
        {
            get
            {
                return ownerID == NetConnector.instance.playerID.value;
            }
        }

        public void WriteData(string dataString)
        {
            OnMessageRecieve.Invoke(dataString);
        }

        public void DeleteObject()
        {
            NetConnector.instance.SendDataToServer(
                JsonUtility.ToJson(new DestroyObjectMessage()
                {
                    eventName = "destroyObject",
                    distributionOption = "serveAll",
                    objectID = id
                })
            );
        }

        public class DestroyObjectMessage
        {
            public string eventName;
            public string distributionOption;
            public string objectID;
        }

    }
}