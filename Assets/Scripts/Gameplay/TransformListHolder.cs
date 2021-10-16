using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TransformListHolder : MonoBehaviour
    {
        [SerializeField] List<Transform> transformList;

        public List<Transform> GetList() {
            return transformList;
        }
    }
}