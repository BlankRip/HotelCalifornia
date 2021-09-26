using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class BlurEffect : MonoBehaviour, IAbilityResetter
    {

        public BlurEffect(float dur)
        {
            Invoke("ResetEffect", dur);
        }

        public void ResetEffect()
        {
            Debug.LogError("RESETTING!!");
            Destroy(this);
        }
    }
}