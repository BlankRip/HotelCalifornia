using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    [CreateAssetMenu()]
    public class ReplicateObjectDatabase : ScriptableObject
    {
        public List<RepObj> objects;
    }

    [System.Serializable]
    public class RepObj
    {
        public GameObject Object;
        public string name;
        public Vector3 originalPos;
        public Quaternion originalRot;
        public void SetOriginal(Vector3 pos, Quaternion rot)
        {
            originalPos = pos;
            originalRot = rot;
        }
    }
}