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
        if(DevBoy.yes) {
            Destroy(this);
            return;
        }
        netObject = GetComponent<NetObject>();
        netObject.OnMessageRecieve += SyncTrans;
    }

    void SyncTrans(string dataString)
    {
        RunOnUpdate = () =>
        {
            TransformData transformData = JsonUtility.FromJson<TransformData>(dataString);
            Vector3 targetPos = transformData.transform.position.ToVector();
            if(targetPos != Vector3.zero)
                transform.position = targetPos;
            try {
                transform.rotation = transformData.transform.rotation.ToQuaternion();
            } catch { }
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