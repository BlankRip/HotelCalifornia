using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.CharacterSelect
{
    public class ModelSwitcher : MonoBehaviour
    {
        public ScriptableCharacterSelect scriptableCharacterSelect;
        public Character currentCharacter;
        Character defaultCharacter;

        private void Awake()
        {
            defaultCharacter = currentCharacter;
            SwitchModel(defaultCharacter);
        }

        public void SwitchModel(Character character)
        {
            if (character != currentCharacter)
            {
                currentCharacter.gameObject.SetActive(false);
                character.gameObject.SetActive(true);
                currentCharacter = character;
                scriptableCharacterSelect.characterType = character.characterType;
                scriptableCharacterSelect.modelType = character.modelType;
            }
        }

        private void OnDisable()
        {
            SwitchModel(defaultCharacter);
        }

        public void Enable()
        {
            SwitchModel(defaultCharacter);
        }
    }
}