using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorsePlayer : MonoBehaviour, IMorseSolutionPlayer
    {
        public int numberOfLetters = 3;
        [SerializeField] List<AudioData> solution;
        ScheduledAudioPlayer myPlayer;

        void Start()
        {
            SetUpPlayer();
        }

        public void SetUpPlayer()
        {
            solution = new List<AudioData>();
            BuildSolution();
            GameObject go = Instantiate(Resources.Load("ScheduledPlayer"), transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            myPlayer = go.GetComponent<ScheduledAudioPlayer>();
            foreach (AudioData data in solution)
            {
                myPlayer.songsToPlay.Add(data.clip);
            }
        }

        public void BuildSolution()
        {
            for (int i = 0; i < numberOfLetters; i++)
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                        solution.Add(AudioPlayer.instance.GetClip(ClipName.MorseA));
                        break;
                    case 2:
                        solution.Add(AudioPlayer.instance.GetClip(ClipName.MorseB));
                        break;
                    case 3:
                        solution.Add(AudioPlayer.instance.GetClip(ClipName.MorseC));
                        break;
                }
            }
        }

        public void Solved()
        {
            Destroy(myPlayer.gameObject);
        }

        public void Link(MorseDevice puzzle)
        {
            foreach (AudioData data in solution)
            {
                puzzle.solution.Add(data.clipName);
            }
        }
    }
}