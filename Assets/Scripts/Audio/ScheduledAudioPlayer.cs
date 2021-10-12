using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    public class ScheduledAudioPlayer : MonoBehaviour
    {
        private double startTime;
        private int toggle = 0;
        private int nextClip = 0;
        public AudioSource[] musicSources;
        public List<AudioClip> songsToPlay;

        private void Start()
        {
            startTime = AudioSettings.dspTime;
        }

        void Update()
        {
            if (AudioSettings.dspTime > startTime - 1)
            {
                Debug.Log("playing");
                //Check if there is an empty audio Clip
                if (songsToPlay[nextClip] != null)
                {
                    AudioClip clipToPlay = songsToPlay[nextClip];
                    // Loads the next Clip to play and schedules when it will start
                    musicSources[toggle].clip = clipToPlay;
                    musicSources[toggle].PlayScheduled(startTime);
                    // Checks how long the Clip will last and updates the Next Start Time with a new value
                    double duration = (double)clipToPlay.samples / clipToPlay.frequency;
                    startTime = startTime + duration;
                    // Switches the toggle to use the other Audio Source next
                    toggle = 1 - toggle;
                }
                // Increase the clip index number, reset if it runs out of clips
                nextClip = nextClip < songsToPlay.Count - 1 ? nextClip + 1 : 0;
            }
        }
    }
}