using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Knotgames.CharacterData;
using UnityEngine.UI;

public class InGameVivox : MonoBehaviour
{
    [SerializeField] ScriptableCharacterSelect charData;
    [SerializeField] SOString roomId;
    [SerializeField] Image micIndicator;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite unMuteSprite;
    private bool mute;
    private bool loggedIn;
    private VoIPManager theScript;

    private void Start() {
        if(DevBoy.yes)
            MyDestroy();

        if(charData.characterType == CharacterType.Human) {
            theScript = GetComponent<VoIPManager>();
            VoIPManager.channelName = roomId.value;
            theScript.enabled = true;
            loggedIn = true;
        } else 
            MyDestroy();
    }

    private void MyDestroy() {
        Destroy(micIndicator.gameObject);
        Destroy(this.gameObject);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.M)) {
            theScript.Mute();
            ToggleVisual();
        }
    }

    private void ToggleVisual() {
        mute = !mute;
        if(micIndicator != null) {
            if(mute)
                micIndicator.sprite = muteSprite;
            else
                micIndicator.sprite = unMuteSprite;
        }
    }

    private void OnDestroy() {
        if(loggedIn)
            theScript.Logout();
    }

}
