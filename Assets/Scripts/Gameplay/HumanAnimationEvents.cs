using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class HumanAnimationEvents : MonoBehaviour
    {
        AudioSource footSource;
        [SerializeField] AudioClip footstep;

        void Start()
        {
            footSource = GetComponentInChildren<AudioSource>();
        }

        void PlayFootstep()
        {
            footSource.PlayOneShot(footstep);
            footSource.pitch = Random.Range(1, 1.25f);
        }
    }
}