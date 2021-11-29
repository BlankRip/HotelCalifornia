using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using System;

namespace Knotgames.CharacterSelect
{
    public class ModelSwitcher : MonoBehaviour
    {
        public ScriptableCharacterSelect scriptableCharacterSelect;
        public Character currentCharacter;
        Character defaultCharacter;

        private void Start()
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

        public void Enable()
        {
            SwitchModel(defaultCharacter);
        }

        internal void OverrideDefault(Character humanDefault)
        {
            defaultCharacter = humanDefault;
            currentCharacter = defaultCharacter;
            SwitchModel(currentCharacter);
        }
    }
}