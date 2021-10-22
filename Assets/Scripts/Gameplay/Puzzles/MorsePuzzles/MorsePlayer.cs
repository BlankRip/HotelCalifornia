using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Audio;
using Knotgames.Gameplay.Abilities;
using Knotgames.Network;

namespace Knotgames.Gameplay.Puzzle.Morse
{
    public class MorsePlayer : MonoBehaviour, IInterfear
    {
        [SerializeField] ScriptableHostStatus hostStatus;
        [SerializeField] ScriptableMorsePuzzle morsePuzzle;
        [SerializeField] GameObject staticAudioSource;
        [SerializeField] AudioClip alphaClip;
        [SerializeField] AudioClip betaClip;
        [SerializeField] AudioClip omagaClip;

        ScheduledAudioPlayer myPlayer;
        private List<char> playChar;
        [SerializeField] List<AudioClip> clipsToPlay;
        private List<AudioClip> clipType;
        private bool takeTwo;
        private bool canInterfear;
        private DataToSend dataToSend;

        private void Start() {
            GetDataToPlay();
            UpdateClipsToPlay();
            SetUpMoresPlayer();

            canInterfear = true;
            dataToSend = new DataToSend();
            if(!DevBoy.yes)
                NetUnityEvents.instance.morsePlayerEvent.AddListener(RecieveData);
        }

        private void SendData() {
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(dataToSend));
        }

        private void RecieveData(string recieved) {
            PlayStatic();
        }

        private void OnDestroy() {
            if(!DevBoy.yes)
                NetUnityEvents.instance.morsePlayerEvent.RemoveListener(RecieveData);
        }

        private void GetDataToPlay() {
            takeTwo = TakeTwoSounds();
            
            List<int> availableIndex = new List<int>{0, 1, 2};
            List<int> myIndex = new List<int>();
            int iterations = 1;
            if(takeTwo)
                iterations = 2;
            for (int i = 0; i < iterations; i++) {
                int rand = Random.Range(0, availableIndex.Count);
                myIndex.Add(availableIndex[rand]);
                availableIndex.RemoveAt(rand);
            }

            Dictionary<char, AudioData> solDictianary = morsePuzzle.manager.GetSolutionDictionary();
            List<char> chars = new List<char>();
            foreach (KeyValuePair<char, AudioData> item in solDictianary)
                chars.Add(item.Key);

            playChar = new List<char>();
            if(!hostStatus.isHost)
                myIndex = availableIndex;
            foreach(int value in myIndex)
                playChar.Add(chars[value]);
        }

        private bool TakeTwoSounds() {
            int rand = Random.Range(0, 101);
            if(rand < 20 || (rand > 55 && rand < 69) || rand > 89)
                return true;
            else
                return false;
        }

        private void UpdateClipsToPlay() {
            List<char> solution = morsePuzzle.manager.GetSolution();
            Dictionary<char, AudioData> solDictianary = morsePuzzle.manager.GetSolutionDictionary();
            List<int> positionIndex = new List<int>();
            for (int i = 0; i < playChar.Count; i++)
                positionIndex.Add(solution.IndexOf(playChar[i]));
            
            clipsToPlay = new List<AudioClip>();
            for (int i = 0; i < positionIndex.Count; i++) {
                clipsToPlay.Add(GetPositionClip(positionIndex[i]));
                clipsToPlay.Add(solDictianary[playChar[i]].clip);
            }
        }

        private AudioClip GetPositionClip(int postion) {
            switch(postion) {
                case 0:
                    return alphaClip;
                case 1:
                    return betaClip;
                case 2:
                    return omagaClip;
            }
            return null;
        }

        public void SetUpMoresPlayer() {
            GameObject go = Instantiate(Resources.Load("ScheduledPlayer"), transform.position, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
            myPlayer = go.GetComponent<ScheduledAudioPlayer>();
            foreach (AudioClip clip in clipsToPlay)
                myPlayer.songsToPlay.Add(clip);
        }

        public bool CanInterfear() {
            return canInterfear;
        }

        public void Interfear() {
            PlayStatic();
        }

        private void PlayStatic() {
            myPlayer.gameObject.SetActive(false);
            staticAudioSource.SetActive(true);
            canInterfear = false;
            StopAllCoroutines();
            StartCoroutine(StopStatic());
        }

        private IEnumerator StopStatic() {
            yield return new WaitForSeconds(8);
            staticAudioSource.SetActive(false);
            myPlayer.gameObject.SetActive(true);
            canInterfear = true;
        }

        private class DataToSend {
            public string eventName;
            public string roomID;
            public string distributionOption;

            public DataToSend() {
                this.eventName = "interferePlayer";
                distributionOption = DistributionOption.serveOthers;
                if(!DevBoy.yes)
                    roomID = NetRoomJoin.instance.roomID.value;
            }
        }
    }
}