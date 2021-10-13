using System.Collections;
using System.Collections.Generic;
using Knotgames.Audio;
using UnityEngine;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorsePlayer : MonoBehaviour
    {
        ScheduledAudioPlayer myPlayer;

        public void SetUpMoresPlayer(Dictionary<char, AudioData> clipDictionary) {
            GameObject go = Instantiate(Resources.Load("ScheduledPlayer"), transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            myPlayer = go.GetComponent<ScheduledAudioPlayer>();
            foreach (KeyValuePair<char, AudioData> pair in clipDictionary)
                myPlayer.songsToPlay.Add(pair.Value.clip);
        }

        public void Solved()
        {
            Destroy(myPlayer.gameObject);
        }
    }
}