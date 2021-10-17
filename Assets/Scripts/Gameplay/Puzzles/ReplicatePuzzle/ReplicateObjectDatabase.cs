using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Replicate
{
    public class ReplicateObjectDatabase : ScriptableObject
    {
        public List<RepObj> objects;
    }

    public class RepObj
    {
        public GameObject Object;
        public string name;
        public Vector3 originalPos;
        public Quaternion originalRot;
        public void ResetToOriginal()
        {
            Object.transform.SetPositionAndRotation(originalPos, originalRot);
        }
    }
}