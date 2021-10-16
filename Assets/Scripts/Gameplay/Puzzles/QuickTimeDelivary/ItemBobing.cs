using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.QuickDelivery {
    public class ItemBobing : MonoBehaviour
    {
        [Range(0, 0.5f)] [SerializeField] float bobDistance;
        [SerializeField] float moveSpeed = 3;
        private List<Vector3> targetPos;
        private int currentIndex;
        private float distance;
        private float distanceCheck;

        private void Start() {
            SetUpTargetPos();
            currentIndex = 0;
            distanceCheck = 0.02f * 0.02f;
        }

        private void Update() {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos[currentIndex], Time.deltaTime * moveSpeed);
            distance = (transform.localPosition - targetPos[currentIndex]).sqrMagnitude;
            if(distance < distanceCheck) {
                if(currentIndex != targetPos.Count - 1)
                    currentIndex++;
                else
                    currentIndex = 0;
            }
        }

        private void SetUpTargetPos() {
            targetPos = new List<Vector3>();
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            targetPos.Add(Vector3.zero);
            List<int> availableIndex = new List<int>{0, 1, 2, 3, 4, 5, 6, 7};
            Vector3 temp = new Vector3(bobDistance, bobDistance, bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(-bobDistance, -bobDistance, -bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(-bobDistance, bobDistance, bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(bobDistance, -bobDistance, -bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(-bobDistance, bobDistance, -bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(bobDistance, -bobDistance, bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(bobDistance, bobDistance, -bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
            temp = new Vector3(-bobDistance, -bobDistance, bobDistance);
            InscertInRandomIndex(ref availableIndex, temp);
        }

        private void InscertInRandomIndex(ref List<int> indexes, Vector3 value) {
            int rand = Random.Range(0, indexes.Count);
            targetPos[rand] = value;
            indexes.RemoveAt(rand);
        }
    }
}