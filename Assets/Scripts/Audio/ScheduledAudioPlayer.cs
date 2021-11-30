using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    public class ScheduledAudioPlayer : MonoBehaviour
    {
        public List<AudioClip> songsToPlay = new List<AudioClip>();
        public AudioSource audioSource;

        private void Update()
        {
            if (audioSource.isPlaying == false)
            {
                AudioClip clip = songsToPlay[0];
                audioSource.clip = clip;
                songsToPlay.RemoveAt(0);
                songsToPlay.Add(clip);
                audioSource.Play((ulong)clip.frequency * 5);
            }
        }
    }
}