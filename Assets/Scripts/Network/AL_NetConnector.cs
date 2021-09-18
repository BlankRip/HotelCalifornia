
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using WebSocketSharp;

// using System.Text;

// using Newtonsoft.Json;

// namespace Knotgames.Alwin.Network
// {

//     public delegate void OnMessageRecive(string data);


//     public class AL_NetConnector : MonoBehaviour, INetConnector
//     {

//         public ScriptableNetConnector netConnector;
//         public ScriptableNetObjectManager netObjectManager;

//         public string playerID;
//         public string roomID;

//         public bool testing;

//         public string ipLocal, ipAddressString;
//         public int portNumber;


//         OnMessageRecive OnMessageRecivePoint;

//         public WebSocket ws;

//         public ScriptableNetEventList eventReciver;
//         public IJoinRoom joinRoom;
//         public INetEvent netEvent;

//         void Awake()
//         {
//             netConnector.value = this;
//         }

        // void OnDestroy()
        // {
        //     ws?.Close(CloseStatusCode.Normal, JsonConvert.SerializeObject(new
        //     {
        //         playerID = playerID
        //     }));
        // }

//         void Start()
//         {
//             Invoke("Connect", 0.2f);
//             joinRoom = new RoomJoiner(this);
//             netEvent = new AL_NetEventReciver(this, netObjectManager.value);
//             eventReciver.value = netEvent;
//         }

//         public void Connect()
//         {
//             Debug.Log("Connection attempt...");

//             string connectionURL = !testing ? ipAddressString : $"{ipLocal}:{portNumber}";
//             ws = new WebSocket($"ws://{connectionURL}");
//             ws.OnMessage += (sender, e) => DataReciver(e);
//             OnMessageRecivePoint += netEvent.EventList;
//             ws.Connect();
//         }

//         public void JoinRoom()
//         {
//             joinRoom.JoinRoom();
//         }


//         public void SetPlayerID(string id)
//         {
//             playerID = id;
//             Debug.Log("Got It..." + id);
//         }

//         public void SetRoomID(string id)
//         {
//             roomID = id;
//         }

//         public string GetPlayerID()
//         {
//             return playerID;
//         }

//         /*

//         public void JoinRandomRoom()
//         {

//         }

//         public void JoinRoomWithID()
//         {

//         }

//         public void CreateRoom()
//         {
//             Debug.Log("Join....");
//             ws.Send(AL_NetUtilityFunctions.ReturnJSONString<string>(ServerOpTyp.serveMe, EventNames.joinRandom, null));
//         }

//         */
//         public WebSocket GetWebsocket()
//         {
//             return ws;
//         }

//         public void SubscribeListener(OnMessageRecive action)
//         {
//             OnMessageRecivePoint += action;
//         }
//         public void UnsubscribeListener(OnMessageRecive action)
//         {
//             OnMessageRecivePoint -= action;
//         }

//         public static byte[] PackByteData(ServerOpTyp oprationName, EventNames eventname, TestStorePacket obj)
//         {
//             return DataArray(
//                 new BytePacket(new string[2]{
//                 ((EventNames)eventname).ToString(),
//                  ((ServerOpTyp)oprationName).ToString()
//                     }, obj)
//             );
//         }

//         public static byte[] PackByteData(ref BytePacket packet, ServerOpTyp oprationName, EventNames eventname, TestStorePacket obj)
//         {
//             packet.headerParams = new string[2]{
//                 ((EventNames)eventname).ToString(),
//                  ((ServerOpTyp)oprationName).ToString()
//                 };
//             packet.data = obj;
//             return DataArray(packet);
//         }

//         public static byte[] DataArray(System.Object obj)
//         {
//             //  return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj),);
//             return Encoding.UTF8.GetBytes("Hi all my name s paul..");
//         }

//         public class Return_Base
//         {
//             public string eventName;
//         }

//         public class Return_OnJoinConfirm : Return_Base
//         {
//             public string playerID;
//         }

//         void DataReciver(MessageEventArgs eventData)
//         {
//             string val = Encoding.UTF8.GetString(eventData.RawData);
//             Debug.Log("Binarry to JSON : " + val);
//             OnMessageRecivePoint.Invoke(val);
//         }

//     }

//     public enum EventNames
//     {
//         connect,
//         joinRandom,
//         joinRoomID,
//         spawnObject,
//         destroyObject,
//         transformUpdt,
//         dataShare,
//         general,
//         objectData
//     }

//     public enum ServerOpTyp
//     {
//         serveOthers, // all player except me
//         serveAll, // all incliding me
//         serveMe, // only me
//         oneDir, // send data to server
//     }

//     public class RecivedEvent
//     {
//         public string eventName;
//     }

//     [System.Serializable]
//     public class BytePacket
//     {
//         public string[] headerParams;
//         public TestStorePacket data;

//         public BytePacket()
//         {
//         }

//         public BytePacket(string[] headerPrams, TestStorePacket dataObject)
//         {
//             this.headerParams = headerPrams;
//             this.data = dataObject;
//         }

//         public BytePacket(TestStorePacket dataObject)
//         {
//             this.data = dataObject;
//         }

//     }
// }