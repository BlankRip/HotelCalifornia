using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.CharacterData
{
    public enum CharacterType
    {
        Player, Ghost
    }

    public enum ModelType
    {
        Human1, Human2, Human3, Human4,
        Ghost1, Ghost2
    }

    public enum AbilityType
    {
        test1, test2, test3, test4, test5, test6
    }

    [CreateAssetMenu()]
    public class ScriptableCharacterSelect : ScriptableObject
    {
        public CharacterType characterType;
        public ModelType modelType;
        public List<AbilityType> abilityTypes;
    }
}