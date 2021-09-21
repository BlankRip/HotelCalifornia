using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Gameplay;

namespace Knotgames.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer instance;

        [SerializeField] ScriptableAudioDatabase audioDatabase;
        [SerializeField] AudioSource sfxSource2D;
        private AudioSource sfxSource3D;

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else
                Destroy(this);

            sfxSource2D.loop = false;
            sfxSource2D.playOnAwake = false;
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

        public void PlayAudio3D(ClipName name, Vector3 position)
        {
            sfxSource3D = ObjectPool.instance.SpawnPoolObj("3dSource", position, Quaternion.identity).GetComponent<AudioSource>();
            sfxSource3D.Stop();
            sfxSource3D.clip = audioDatabase.GetClip(name);
            sfxSource3D.Play();
        }

        public void PlayAudio3DOneShot(ClipName name)
        {
            if(sfxSource3D != null)
                sfxSource3D.PlayOneShot(audioDatabase.GetClip(name));
        }
    }
}