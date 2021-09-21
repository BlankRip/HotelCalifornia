using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    [System.Serializable]
    public class AudioData
    {
        public ClipName clipName;
        public AudioClip clip;
    }

    public enum ClipName
    {
        Nada, Test1, Test2
    }
}