using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class MoveTrapEffect : MonoBehaviour
    {
        IGhostMoveAdjustment ghostMoveAdjustment;
        float timer;
        GameObject trapObj;
        bool onTimer;

        void Start()
        {
            ghostMoveAdjustment = GetComponent<IGhostMoveAdjustment>();
        }

        private void Update()
        {
            if (onTimer)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    onTimer = false;
                    ghostMoveAdjustment.LockMovement(false);
                    Destroy(trapObj);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GhostLocker"))
            {
                ghostMoveAdjustment.LockMovement(true);
                trapObj = other.transform.parent.GetComponent<IMovementTrap>().GetGameObject();
                timer = 10;
                onTimer = true;
            }
        }
    }
}