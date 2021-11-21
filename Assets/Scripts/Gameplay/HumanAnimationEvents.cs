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
            Debug.Log("FOOTED");
            footSource.PlayOneShot(footstep);
            footSource.pitch = Random.Range(1, 1.25f);
        }
    }
}