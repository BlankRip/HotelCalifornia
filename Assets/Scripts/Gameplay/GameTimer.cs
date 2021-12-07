using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Knotgames.Audio;

namespace Knotgames.Gameplay {
    public class GameTimer : MonoBehaviour
    {
        [SerializeField] GameplayEventCollection gameplayEvents;
        [SerializeField] int minutes, seconds;
        [SerializeField] TextMeshProUGUI minutesText, secondsText;
        [SerializeField] Material visualMat;
        private int currenMinutes;
        private float currentSeconds;
        private bool timerOn;
        private float visualValue;
        private float maxSeconds;
        private float currentVisualSec;

        private void Awake() {
            currenMinutes = minutes;
            minutesText.text = minutes.ToString();
            currentSeconds = seconds;
            secondsText.text = seconds.ToString();
            gameplayEvents.gameStart.AddListener(StartTimer);
            maxSeconds = (60 * minutes) + seconds;
            currentVisualSec = maxSeconds;
            visualMat.SetFloat("_T", 0);
        }

        private void OnDestroy() {
            gameplayEvents.gameStart.RemoveListener(StartTimer);
        }

        private void Update() {
            //TODO Remove from final build
            #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.H)) {
                gameplayEvents.gameStart.Invoke();
            }
            #endif
            if(timerOn) {
                if(currentSeconds > 0) {
                    currentSeconds -= Time.deltaTime;
                    currentVisualSec -= Time.deltaTime;
                }
                else {
                    if(currenMinutes > 0) {
                        currenMinutes--;
                        minutesText.text = currenMinutes.ToString();
                        currentSeconds = 60;
                        currentSeconds -= Time.deltaTime;
                        currentVisualSec -= Time.deltaTime;
                    } else {
                        timerOn = false;
                        AudioPlayer.instance.PlayAudio2DOneShot(ClipName.TimeUp);
                        Debug.Log("Gameover: Ghost wins");
                    }
                }
                secondsText.text = ((int)currentSeconds).ToString();
                VisualUpdate();
            }
        }

        private void VisualUpdate() {
            visualValue = Mathf.InverseLerp(maxSeconds, 0, currentVisualSec);
            visualMat.SetFloat("_T", visualValue);
        }

        private void StartTimer() {
            timerOn = true;
        }
    }
}