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
        Nada, MorseA, MorseB, MorseC, MorseD, MorseE, MorseF, MorseG, MorseH, MorseI,
        Lever, MapConnection, MapPiece, Radio, Numpad, Solved, RightAnswer, WrongAnswer, TimeUp
    }
}