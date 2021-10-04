using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class ObjectPool : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public int maxSize;
            public Transform parent;
        }

        [SerializeField] List<Pool> objPools;
        Dictionary<string, Queue<GameObject>> poolDictionary;
        GameObject objToSpawn;

        [HideInInspector] public static ObjectPool instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in objPools)
            {
                Queue<GameObject> objectQue = new Queue<GameObject>();

                for (int i = 0; i < pool.maxSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    if(pool.parent != null)
                        obj.transform.parent = pool.parent;
                    obj.SetActive(false);
                    objectQue.Enqueue(obj);
                }
                poolDictionary.Add(pool.tag, objectQue);
            }
        }

        public GameObject SpawnPoolObj(string tag, Vector3 position, Quaternion rotation)
        {
            if(!poolDictionary.ContainsKey(tag))
            {
                Debug.Log("<color=red> Object pool with " + tag + " tag does not exist </color>");
                return null;
            }

            objToSpawn = poolDictionary[tag].Dequeue();

            objToSpawn.transform.position = position;
            objToSpawn.transform.rotation = rotation;
            objToSpawn.SetActive(true);

            poolDictionary[tag].Enqueue(objToSpawn);

            return objToSpawn;
        }
    }
}