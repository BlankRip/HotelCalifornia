using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Knotgames.Network
{

    public class NetObjManager : MonoBehaviour, INetObjectManager
    {

        public static NetObjManager instance;

        public static Hashtable allNetObjectDictionary = new Hashtable();
        public List<NetObject> allNetObject = new List<NetObject>();
        public static Queue<Action> networkActionQueue = new Queue<Action>();
        NetObject tempObj;

        void Awake()
        {
            instance = this;
        }

        void Update()
        {
            while (networkActionQueue.Count > 0)
            {
                networkActionQueue.Dequeue().Invoke();
            }
        }

        public int GetNetObjectCount()
        {
            return allNetObject.Count;
        }

        public void DestroyObjectAtIndex(int index)
        {
            allNetObject[index].DeleteObject();
        }

        public void AddSpawnObject(string dataObj)
        {
            SpawnObject spawnObject = JsonUtility.FromJson<SpawnObject>(dataObj);

            networkActionQueue.Enqueue(() =>
            {
                // find object 
                Debug.Log(spawnObject.objectName);
                GameObject newObject = Instantiate(Resources.Load(spawnObject.objectName) as GameObject,
                spawnObject.transformWS.position.ToVector(),
                spawnObject.transformWS.rotation.ToQuaternion());
                NetObject newNetObject = newObject.GetComponent<NetObject>();
                if (newNetObject == null) newNetObject = newObject.AddComponent<NetObject>();

                allNetObject.Add(newNetObject);
                newNetObject.ownerID = spawnObject.ownerID;
                newNetObject.isOwner = newNetObject.IsMine;
                newNetObject.id = spawnObject.id;
                allNetObjectDictionary.Add(newNetObject.id, newNetObject);
            });
        }


        public void AddDespawnObject(NetDataBase dataObj)
        {
            networkActionQueue.Enqueue(() =>
            {
                tempObj = allNetObjectDictionary[dataObj.id] as NetObject;
                allNetObjectDictionary.Remove(dataObj.id);
                allNetObject.Remove(tempObj);
                if (tempObj.gameObject) Destroy(tempObj.gameObject);
            });
        }

        public void PassDataToObject(string objectID, string dataString)
        {
            (allNetObjectDictionary[objectID] as INetObject).WriteData(dataString);
        }

        [System.Serializable]
        public class SpawnObject
        {
            public string eventName;
            public string objectName;
            public string ownerID;
            public string id;
            public TransformWS transformWS;
        }

    }

}