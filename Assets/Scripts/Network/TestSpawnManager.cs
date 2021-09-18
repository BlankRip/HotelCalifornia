using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Network
{

    public class TestSpawnManager : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                NetConnector.instance.SendDataToServer(
                    JsonUtility.ToJson(
                        new SpawnObject()
                        {
                            eventName = "spawnObject",
                            distributionOption = "serveAll",
                            ownerID = NetConnector.instance.playerID.value,
                            objectName = "testCube",
                            transformWS = new TransformWS()
                            {
                                position = new PositionWS(Vector3.one),
                                rotation = new RotationWS(Quaternion.identity)
                            }
                        }
                    )
                );
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject newObject = Instantiate(Resources.Load("testCube") as GameObject);
            }
        }

        public class SpawnObject
        {
            public string eventName;
            public string distributionOption;
            public string ownerID;
            public string objectName;
            public TransformWS transformWS;
        }

    }
}