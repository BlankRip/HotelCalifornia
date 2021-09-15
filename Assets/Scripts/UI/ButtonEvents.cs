using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.UI
{
    public class ButtonEvents : MonoBehaviour
    {
        [SerializeField] TMP_Text text;
        [SerializeField] AudioClip clip;
        [SerializeField] AudioSource source;
        bool first;
        Animator myAnim;

        private void Awake()
        {
            myAnim = GetComponent<Animator>();
        }

        public void SetColor()
        {
            myAnim.SetBool("hover", true);
        }

        public void ResetColor()
        {
            myAnim.SetBool("hover", false);
        }

        public void Hover()
        {
            source.Stop();
            source.PlayOneShot(clip);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void OnEnable()
        {
            if (first)
                ResetColor();
        }
    }
}