using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;

namespace Knotgames.Gameplay {
    public class BufferActivator : MonoBehaviour
    {
        [SerializeField] GameObject myBuffer;
        [SerializeField] ScriptableCharacterSelect characterData;
        [SerializeField] CharacterType myBufferType;

        private void Awake() {
            if(characterData.characterType == myBufferType)
                myBuffer.SetActive(true);
        }
    }
}