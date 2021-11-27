using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.Network;

namespace Knotgames.LevelGen
{
    public class LevelSeed : MonoBehaviour, ILevelSeed
    {
        [SerializeField] ScriptableLevelSeed seed;
        private int seedValue;
        private bool host;

        private void Awake() {
            seed.levelSeed = this;
            if(DevBoy.yes)
                GenerateSeed();
        }

        public void Initilize() {
            KnotRandom.theRand = new System.Random(seedValue);
        }

        public void GenerateSeed() {
            host = true;
            seedValue = Random.Range(int.MinValue, int.MaxValue);
        }

        public void SeedSuccesful() {
            if(!DevBoy.yes) {
                if(host)
                    SendSeed();
                SendLevelReady();
            }
        }

        private void SendSeed() {
            Debug.LogError("Sending seed");
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new SeedPacket(seedValue)));
        }

        private void SendLevelReady() {
            Debug.LogError("SET Level gen COMPTLET");
            NetConnector.instance.SendDataToServer(JsonUtility.ToJson(new ReadyData("setupCompleated", DistributionOption.serveMe)));
        }

        public void SetSeed(int seed) {
            seedValue = seed;
        }

        public int GetSeed() {
            return seedValue;
        }
    }
}