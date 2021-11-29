using System.Collections;
using System.Collections.Generic;
using Knotgames.CharacterData;
using Knotgames.CharacterSelect;
using Knotgames.Network;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class TestCharSelect : MonoBehaviour
    {
        [SerializeField] bool useServer;
        [SerializeField] bool human;
        [SerializeField] GameObject humanGroup, ghostGroup;
        [SerializeField] Character humanDefault, ghostDefault;
        [SerializeField] ScriptableCharacterSelect scriptableCharSelect;
        ModelSwitcher modelSwitcher;

        void Start()
        {
            modelSwitcher = FindObjectOfType<ModelSwitcher>();
            if(!useServer)
                RandomGroup();
            if (useServer)
                NetConnector.instance.OnMsgRecieve.AddListener(Hear);
        }

        private void OnDestroy()
        {
            if (useServer)
                NetConnector.instance.OnMsgRecieve.RemoveListener(Hear);
        }

        private void RandomGroup()
        {
            scriptableCharSelect.ResetAbilityTypes();
            switch (human)
            {
                case true:
                    Debug.Log("human");
                    humanGroup.SetActive(true);
                    modelSwitcher.OverrideDefault(humanDefault);
                    scriptableCharSelect.characterType = CharacterType.Human;
                    scriptableCharSelect.modelType = ModelType.Human1;
                    ghostGroup.SetActive(false);
                    break;
                case false:
                    Debug.Log("ghost");
                    humanGroup.SetActive(false);
                    modelSwitcher.OverrideDefault(ghostDefault);
                    scriptableCharSelect.characterType = CharacterType.Ghost;
                    scriptableCharSelect.modelType = ModelType.Ghost1;
                    ghostGroup.SetActive(true);
                    break;
            }
        }

        public void Hear(string dataString)
        {
            string eventName = JsonUtility.FromJson<ReadyData>(dataString).eventName;
            switch (eventName)
            {
                case "roomFull":
                    {
                        scriptableCharSelect.ResetAbilityTypes();
                        string playerType = JsonUtility.FromJson<PlayerTypeExtractor>(dataString).playerType;
                        switch (playerType)
                        {
                            case "human":
                                Debug.Log("human");
                                humanGroup.SetActive(true);
                                modelSwitcher.OverrideDefault(humanDefault);
                                scriptableCharSelect.characterType = CharacterType.Human;
                                scriptableCharSelect.modelType = ModelType.Human1;
                                ghostGroup.SetActive(false);
                                break;
                            case "ghost":
                                Debug.Log("ghost");
                                humanGroup.SetActive(false);
                                modelSwitcher.OverrideDefault(ghostDefault);
                                scriptableCharSelect.characterType = CharacterType.Ghost;
                                scriptableCharSelect.modelType = ModelType.Ghost1;
                                ghostGroup.SetActive(true);
                                break;
                        }
                    }
                    break;
            }
        }
    }
}