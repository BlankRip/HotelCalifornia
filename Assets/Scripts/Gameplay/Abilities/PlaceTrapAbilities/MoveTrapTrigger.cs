using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay.UI;

namespace Knotgames.Gameplay.Abilities {
    public class MoveTrapTrigger : MonoBehaviour, IAbility
    {
        private int usesLeft;
        private IAbilityUi myUi;

        private ITrapRayRequirements requirements;
        private ScriptableRayCaster rayCaster;
        private RaycastHit hitInfo;

        private bool placing;
        private bool movingTrap;
        private LayerMask currentLayerMask;
        private IMovementTrap trap;
        private Vector3 trapPos;
        private float trapPlaceRadius = 20;

        private void Start() {
            requirements = GetComponent<ITrapRayRequirements>();
            myUi = GameObject.FindGameObjectWithTag("PrimaryUi").GetComponent<IAbilityUi>();
            usesLeft = 1;
            myUi.UpdateObjectData(usesLeft);
            trapPlaceRadius = trapPlaceRadius * trapPlaceRadius;
            rayCaster = requirements.GetRayCaster();
        }

        private void Update() {
            if(placing) {
                bool outOfBound = OutOfBoundsCheck();
                if(outOfBound) return;
                
                hitInfo = rayCaster.caster.CastRay(currentLayerMask, 20);
                if(hitInfo.collider != null) {
                    if(movingTrap)
                        trap.MoveTrapTo(hitInfo.point);
                    else
                        trap.MoveNutralizerTo(hitInfo.point, hitInfo.normal);
                }

                if(Input.GetKeyDown(KeyCode.Mouse1)) {
                    if(movingTrap)
                        SetPredictedTrap();
                    else
                        CompleteUse();
                }

            }
        }

        private void SetPredictedTrap() {
            movingTrap = false;
            trapPos = hitInfo.point;
            currentLayerMask = requirements.GetPlacableLayers();
        }

        private void CompleteUse() {
            trap.SetTrap();
            placing = false;
            usesLeft--;
            myUi.UpdateObjectData(usesLeft);
        }

        private bool OutOfBoundsCheck() {
            if(!movingTrap) {
                Vector3 playerPos = transform.position;
                playerPos.y = trapPos.y;
                float distance = (playerPos - trapPos).sqrMagnitude;
                if(distance >= trapPlaceRadius) {
                    placing = false;
                    Destroy(trap.GetGameObject());
                    return true;
                } else
                    return false;
            } else {
                return false;
            }
        }

        public bool CanUse() {
            if(usesLeft != 0 && !placing)
                return true;
            else
                return false;
        }

        public void UseAbility() {
            trap = GameObject.Instantiate(requirements.GetTrapObj()).GetComponent<IMovementTrap>();
            placing = true;
            currentLayerMask = requirements.GetGroundLayers();
            movingTrap = true;
        }

        public void Destroy() {
            Destroy(this);
        }
    }

}