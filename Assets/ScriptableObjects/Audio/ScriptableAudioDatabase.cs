using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    [CreateAssetMenu()]
    public class ScriptableAudioDatabase : ScriptableObject
    {
        public List<AudioData> audioData;
        private Dictionary<ClipName, AudioData> audioDictonary;
        
        public AudioClip GetClip(ClipName name)
        {
            if(audioDictonary == null || audioDictonary.Count != audioData.Count)
                FillDictionary();
            
            return audioDictonary[name].clip;
        }

        public AudioData GetClipData(ClipName name)
        {
            if(audioDictonary == null || audioDictonary.Count != audioData.Count)
                FillDictionary();
            
            return audioDictonary[name];
        }

        private void FillDictionary() {
            audioDictonary = new Dictionary<ClipName, AudioData>();
            foreach (AudioData data in audioData)
                audioDictonary.Add(data.clipName, data);
        }
    }
}