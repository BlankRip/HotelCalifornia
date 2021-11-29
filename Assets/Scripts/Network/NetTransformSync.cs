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
    int elapsedFrames = 0;
    public int interpolationFramesCount = 45;

    void Start()
    {
        if (DevBoy.yes)
        {
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
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);
            TransformData transformData = JsonUtility.FromJson<TransformData>(dataString);
            Vector3 targetPos = transformData.transform.position.ToVector();
            if (targetPos != Vector3.zero)
                transform.position = Vector3.Lerp(transform.position, targetPos, interpolationRatio);
            try
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, transformData.transform.rotation.ToQuaternion(), interpolationRatio);
            }
            catch { }
        };
    }

    void Update()
    {
        if (netObject.IsMine)
        {
            if (NetRoomJoin.instance.roomID.value != null)
            {
                TransformData dataToSend = new TransformData(netObject.id, transform.position, transform.rotation);
                if (dataToSend != null)
                    NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
                else
                    Debug.LogError("TRASH ENGINE!");
            }
            else
                Debug.LogError("<color=red> CRASH PREVENTED </color>");
        }
        else
        {
            if (RunOnUpdate != null)
                RunOnUpdate.Invoke();
        }
    }
}

[System.Serializable]
public class TransformData
{
    public string distributionOption = DistributionOption.serveOthers;
    public string eventName = "syncObjectData";
    public string ownerID, objectID, roomID;
    public TransformWS transform;
    public TransformData(string id, Vector3 pos, Quaternion rot)
    {
        eventName = "syncObjectData";
        distributionOption = DistributionOption.serveOthers;
        ownerID = NetConnector.instance.playerID.value;
        objectID = id;
        roomID = NetRoomJoin.instance.roomID.value;
        transform = new TransformWS()
        {
            position = new PositionWS(pos),
            rotation = new RotationWS(rot)
        };
    }
}