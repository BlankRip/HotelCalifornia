using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.UI {
    public class ButtonEvents : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        Color original;
        [SerializeField] Color color;
        [SerializeField] AudioClip clip;
        [SerializeField] AudioSource source;
        Vector3 originalScale;
        bool first;

        private void Awake() {
            original = text.color;
            originalScale = text.transform.localScale;
        }

        public void SetColor() {
            text.transform.localScale = originalScale * 2;
            text.color = color;
            first = true;
        }

        public void ResetColor() {
            text.transform.localScale = originalScale;
            text.color = original;
        }

        public void Hover() {
            source.Stop();
            source.PlayOneShot(clip);
        }

        public void Quit() {
            Application.Quit();
        }

        private void OnEnable() {
            if (first)
                ResetColor();
        }
    }
}