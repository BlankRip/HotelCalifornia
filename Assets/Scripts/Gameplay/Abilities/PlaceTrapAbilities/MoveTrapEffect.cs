using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay.Abilities
{
    public class MoveTrapEffect : MonoBehaviour
    {
        IGhostMoveAdjustment ghostMoveAdjustment;
        float timer;
        IMovementTrap trapObj;
        bool onTimer;
        private bool destroying;

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
                    destroying = true;
                    trapObj.DestroyTrap();
                    Invoke("Destroyed", 0.1f);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("GhostLocker") && !destroying)
            {
                ghostMoveAdjustment.LockMovement(true);
                trapObj = other.gameObject.GetComponentInParent<IMovementTrap>();
                timer = 10;
                onTimer = true;
            }
        }

        private void Destroyed() {
            destroying = false;
        }
    }
}