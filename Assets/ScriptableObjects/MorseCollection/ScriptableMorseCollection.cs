using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    [CreateAssetMenu()]
    public class ScriptableMorseCollection : ScriptableObject
    {
        public List<MorseData> moresData;
        private Dictionary<ClipName, string> moresDictionary;
        
        public string GetMorseString(ClipName name)
        {
            if(moresDictionary == null || moresDictionary.Count != moresData.Count)
                FillDictionary();
            
            return moresDictionary[name];
        }

        private void FillDictionary() {
            moresDictionary = new Dictionary<ClipName, string>();
            foreach (MorseData data in moresData)
                moresDictionary.Add(data.clipName, data.clipString);
        }

        [System.Serializable]
        public class MorseData {
            public ClipName clipName;
            public string clipString;
        }
    }
}
