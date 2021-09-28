using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay {
    public class TeleportEffect : MonoBehaviour, IAbilityEffect
    {
        private TeleportPoint[] points;
        private IMoveAdjustment moveAdjustment;

        private void Start() {
            points = FindObjectsOfType<TeleportPoint>();
            moveAdjustment = GetComponent<IMoveAdjustment>();
        }

        public void ApplyEffect() {
            TeleportPoint farthestPoint = null;
            float farthest = float.MinValue;
            float distance;
            for (int i = 0; i < points.Length; i++) {
                distance = Vector3.Distance(transform.position, points[i].transform.position);
                if(distance > farthest) {
                    farthest = distance;
                    farthestPoint = points[i];
                }
            }
            moveAdjustment.MoveToPosition(farthestPoint.GetTeleportPoint());
        }

        public void ResetEffect() { }
    }
}