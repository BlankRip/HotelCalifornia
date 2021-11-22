using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteAlways]
public class PlayerVissionBlur : MonoBehaviour
{

    public static PlayerVissionBlur instance;

    public PostProcessVolume volume;

    PlayerEyeBlur playerEyeBlur;

    public Animation animation;

    public AnimationClip activeClip, closeClip;

    [SerializeField] [Range(0, 1)] float effectStrength;
    [SerializeField] [Range(0, 5)] float vapourStrength;

    public bool updateState;

    public State myState;


    void Start()
    {
        instance = this;
        volume.profile.TryGetSettings(out playerEyeBlur);
    }
    public void SetEffectState(State state)
    {
        myState = state;
        
        switch (myState)
        {
            case State.active:
            animation.clip = activeClip;
            animation.Play();
            break;

            case State.inactive:
            animation.clip = closeClip;
            animation.Play();
            break;
        }
    }

    void Update()
    {
        playerEyeBlur.selectImage.value = effectStrength;
        playerEyeBlur.vapourStrength.value = vapourStrength;
    }

    public void UpdateAnimationState(bool state) 
    {
 
            playerEyeBlur.active = state;

    }


}

public enum State
{
    inactive,
    active
}