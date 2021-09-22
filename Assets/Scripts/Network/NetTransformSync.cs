using System;
using System.Collections;
using System.Collections.Generic;
using Knotgames.Network;
using UnityEngine;

public class NetTransformSync : MonoBehaviour
{
    NetObject netObject;
    System.Action RunOnUpdate;
    Queue<Action> actionList = new Queue<Action>();

    void Start()
    {
        netObject = GetComponent<NetObject>();
        netObject.OnMessageRecieve += SyncTrans;
    }

    void SyncTrans(string dataString)
    {
        RunOnUpdate = () =>
        {
            TransformData transformData = JsonUtility.FromJson<TransformData>(dataString);
            transform.position = transformData.transform.position.ToVector();
            transform.rotation = transformData.transform.rotation.ToQuaternion();
        };
    }

    void Update()
    {
        if (netObject.IsMine)
        {
            NetConnector.instance.SendDataToServer(
                JsonUtility.ToJson(
                    new TransformData()
                    {
                        eventName = "syncObjectData",
                        distributionOption = "serveOthers",
                        ownerID = NetConnector.instance.playerID.value,
                        objectID = netObject.id,
                        roomID = NetRoomJoin.instance.roomID.value,
                        transform = new TransformWS()
                        {
                            position = new PositionWS(transform.position),
                            rotation = new RotationWS(transform.rotation)
                        }
                    }
                )
            );
        }
        else
        {
            if (RunOnUpdate != null) RunOnUpdate.Invoke();
        }
    }
}

[System.Serializable]
public class TransformData
{
    public string distributionOption = "serveOthers";
    public string eventName = "syncObjectData";
    public string ownerID, objectID, roomID;
    public TransformWS transform;
}