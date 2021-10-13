using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;

namespace Knotgames.Gameplay.Puzzle.Morse {
    [CreateAssetMenu()]
    public class ScriptableMorseCollection : ScriptableObject
    {
        public List<MoresData> moresData;
        private Dictionary<ClipName, string> moresDictionary;
        
        public string GetMoresString(ClipName name)
        {
            if(moresDictionary == null || moresDictionary.Count != moresData.Count)
                FillDictionary();
            
            return moresDictionary[name];
        }

        private void FillDictionary() {
            moresDictionary = new Dictionary<ClipName, string>();
            foreach (MoresData data in moresData)
                moresDictionary.Add(data.clipName, data.clipString);
        }

        [System.Serializable]
        public class MoresData {
            public ClipName clipName;
            public string clipString;
        }
    }
}
