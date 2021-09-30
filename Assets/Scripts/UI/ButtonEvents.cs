using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Knotgames.UI
{
    public class ButtonEvents : MonoBehaviour
    {
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
            if (myAnim != null)
                myAnim.SetBool("hover", true);
            else
                myAnim = GetComponent<Animator>();
        }

        public void ResetColor()
        {
            if (myAnim != null)
                myAnim.SetBool("hover", false);
            else
                myAnim = GetComponent<Animator>();
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