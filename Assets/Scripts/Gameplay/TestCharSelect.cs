using System.Collections;
using System.Collections.Generic;
using Knotgames.CharacterData;
using UnityEngine;

namespace Knotgames.Gameplay
{
    public class TestCharSelect : MonoBehaviour
    {
        [SerializeField] bool human;
        [SerializeField] GameObject humanGroup, ghostGroup;
        [SerializeField] ScriptableCharacterSelect scriptableCharSelect;

        void Start()
        {
            RandomGroup();
        }

        private void RandomGroup()
        {
            scriptableCharSelect.ResetAbilityTypes();
            switch (human)
            {
                case true:
                    Debug.Log("human");
                    humanGroup.SetActive(true);
                    scriptableCharSelect.characterType = CharacterType.Human;
                    scriptableCharSelect.modelType = ModelType.Human1;
                    ghostGroup.SetActive(false);
                    break;
                case false:
                    Debug.Log("ghost");
                    humanGroup.SetActive(false);
                    scriptableCharSelect.characterType = CharacterType.Ghost;
                    scriptableCharSelect.modelType = ModelType.Ghost1;
                    ghostGroup.SetActive(true);
                    break;
            }
        }
    }
}