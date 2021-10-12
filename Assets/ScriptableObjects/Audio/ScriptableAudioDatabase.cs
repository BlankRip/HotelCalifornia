using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    [CreateAssetMenu()]
    public class ScriptableAudioDatabase : ScriptableObject
    {
        public List<AudioData> audioData;

        public AudioClip GetClip(ClipName name)
        {
            foreach (AudioData data in audioData)
            {
                if (data.clipName == name)
                    return data.clip;
            }

            return null;
        }

        public AudioData GetClipData(ClipName name)
        {
            foreach (AudioData data in audioData)
            {
                if (data.clipName == name)
                    return data;
            }

            return null;
        }
    }
}