using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteAlways]
public class PlayerVissiionCorrupt : MonoBehaviour
{

    public static PlayerVissiionCorrupt instance;

    public PostProcessVolume volume;

    PEFXGlitch playerCorrupted;

    public Animation animation;

    public AnimationClip activeClip, closeClip;

    [SerializeField] [Range(0, 20)] float chromaShift;
    [SerializeField] Color fogColor;
    [SerializeField] [Range(0, 5)] float fogStrength;

    public bool updateState;

    public State myState;


    void Start()
    {
        instance = this;
        volume.profile.TryGetSettings(out playerCorrupted);
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
        playerCorrupted.ChromaShift.value = chromaShift;
        playerCorrupted.DistColor.value = fogColor;
        playerCorrupted.DistEffect.value = fogStrength;

        if(updateState)
        {
            UpdateAnimationState(myState == State.active ? true : false);
            updateState = false;
        }

    }

    public void UpdateAnimationState(bool state) 
    {
 
            playerCorrupted.active = state;

    }


}