using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorsePlayer : MonoBehaviour, IMorseSolutionPlayer
    {
        public int numberOfLetters = 3;
        [SerializeField] List<AudioData> audioDatas;
        ScheduledAudioPlayer myPlayer;
        Dictionary<char, AudioClip> clipAlphabetDictionary = new Dictionary<char, AudioClip>();
        List<char> myAlphas = new List<char>();
        List<char> alphabets = new List<char>{'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
        [SerializeField] List<char> solution = new List<char>();

        void Start()
        {
            SetUpPlayer();
        }

        public void SetUpPlayer()
        {
            audioDatas = new List<AudioData>();
            BuildSolution();
            GameObject go = Instantiate(Resources.Load("ScheduledPlayer"), transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            myPlayer = go.GetComponent<ScheduledAudioPlayer>();
            foreach (KeyValuePair<char, AudioClip> pair in clipAlphabetDictionary)
            {
                myPlayer.songsToPlay.Add(pair.Value);
            }
        }

        public void BuildSolution()
        {
            for (int i = 0; i < numberOfLetters; i++)
            {
                char alpha = alphabets[Random.Range(0, alphabets.Count)];
                alphabets.Remove(alpha);
                AudioData data = null;
                switch (Random.Range(1, 4))
                {
                    case 1:
                        data = AudioPlayer.instance.GetClip(ClipName.MorseA);
                        break;
                    case 2:
                        data = AudioPlayer.instance.GetClip(ClipName.MorseB);
                        break;
                    case 3:
                        data = AudioPlayer.instance.GetClip(ClipName.MorseC);
                        break;
                }
                myAlphas.Add(alpha);
                audioDatas.Add(data);
                clipAlphabetDictionary.Add(alpha, data.clip);
            }

            while (myAlphas.Count > 0)
            {
                int rand = Random.Range(0, myAlphas.Count);
                solution.Add(myAlphas[rand]);
                myAlphas.RemoveAt(rand);
            }
        }

        public void Solved()
        {
            Destroy(myPlayer.gameObject);
        }

        public void Link(MorseDevice puzzle)
        {
            foreach (AudioData data in audioDatas)
            {
                puzzle.solution.Add(data.clipName);
            }
        }
    }
}