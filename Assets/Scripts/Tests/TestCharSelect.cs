using System.Collections;
using System.Collections.Generic;
using Knotgames.CharacterData;
using UnityEngine;

public class TestCharSelect : MonoBehaviour
{
    [SerializeField] GameObject humanGroup, ghostGroup;
    [SerializeField] ScriptableCharacterSelect scriptableCharSelect;

    void Start()
    {
        RandomGroup();
    }

    private void RandomGroup()
    {
        int r = Random.Range(0, 2);
        scriptableCharSelect.ResetAbilityTypes();
        switch (r)
        {
            case 0:
                Debug.Log("human");
                humanGroup.SetActive(true);
                scriptableCharSelect.characterType = CharacterType.Human;
                scriptableCharSelect.modelType = ModelType.Human1;
                ghostGroup.SetActive(false);
                break;
            case 1:
                Debug.Log("ghost");
                humanGroup.SetActive(false);
                scriptableCharSelect.characterType = CharacterType.Ghost;
                scriptableCharSelect.modelType = ModelType.Ghost1;
                ghostGroup.SetActive(true);
                break;
        }
    }
}
