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
                        new SpawnObject("Player", new Vector3(Random.Range(-8.75f, 8.75f), 1, 1), Quaternion.identity)
                    )
                );
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject newObject = Instantiate(Resources.Load("testCube") as GameObject);
            }
        }
    }
}