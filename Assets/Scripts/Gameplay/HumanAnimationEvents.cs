using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class HumanAnimationEvents : MonoBehaviour
    {
        [SerializeField] AudioSource footSource;
        [SerializeField] AudioClip footstep;
        private Animator unitySmallPP;

        void Start()
        {
            footSource = GetComponentInChildren<AudioSource>();
            unitySmallPP = GetComponent<Animator>();
        }

        void PlayFootstep() {
            if(unitySmallPP.GetFloat("hSpeed") != 0 || unitySmallPP.GetFloat("vSpeed") != 0)
                FootStepEffect();
        }

        void FootStepEffect() {
            footSource.PlayOneShot(footstep);
            footSource.pitch = Random.Range(0.7f, 1.25f);
        }
    }
}