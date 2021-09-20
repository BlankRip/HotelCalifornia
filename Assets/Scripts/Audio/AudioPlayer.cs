using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer audioPlayer;

        [SerializeField] ScriptableAudioDatabase audioDatabase;
        [SerializeField] AudioSource sfxSource2D;
        [SerializeField] AudioSource sfxSource3D;

        private void Awake()
        {
            sfxSource2D.loop = false;
            sfxSource3D.loop = false;
            sfxSource2D.playOnAwake = false;
            sfxSource3D.playOnAwake = false;
        }

        public void PlayAudio2D(ClipName name)
        {
            sfxSource2D.Stop();
            sfxSource2D.clip = audioDatabase.GetClip(name);
            sfxSource2D.Play();
        }
        
        public void PlayAudio2DOneShot(ClipName name)
        {
            sfxSource2D.PlayOneShot(audioDatabase.GetClip(name));
        }

        public void PlayAudio3D(ClipName name)
        {
            sfxSource3D.Stop();
            sfxSource3D.clip = audioDatabase.GetClip(name);
            sfxSource3D.Play();
        }

        public void PlayAudio3DOneShot(ClipName name)
        {
            sfxSource3D.PlayOneShot(audioDatabase.GetClip(name));
        }
    }
}