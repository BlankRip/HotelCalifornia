using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] Animation animation;
    [SerializeField] AnimationClip clip;

    public void Play()
    {
        animation.clip = clip;
        animation.Play();
    }
}
