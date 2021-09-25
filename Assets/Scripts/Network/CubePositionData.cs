using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;


public class CubePositionData : MonoBehaviour
{

    NetObject netObject;
    string dataRecieved;

    System.Action RunOnUpdate;

    void Start()
    {
        netObject = GetComponent<NetObject>();
        netObject.OnMessageRecieve += GetCubeData;
    }

    void GetCubeData(string dataString)
    {
        RunOnUpdate = () =>
        {
            dataRecieved = dataString;
            CubeData cubeData = JsonUtility.FromJson<CubeData>(dataString);
            transform.position = cubeData.transform.position.ToVector();
        };
    }

    void Update()
    {
        if (netObject.IsMine)
        {
            NetConnector.instance.SendDataToServer(
                JsonUtility.ToJson(
                    new CubeData()
                    {
                        eventName = "syncObjectData",
                        distributionOption = DistributionOption.serveOthers,
                        ownerID = NetConnector.instance.playerID.value,
                        objectID = netObject.id,
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


    public class CubeData
    {
        public string eventName;
        public string distributionOption;
        public string ownerID;
        public string objectID;
        public TransformWS transform;
    }

}
