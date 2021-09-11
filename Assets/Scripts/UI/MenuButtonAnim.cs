using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Knotgames.UI
{
    public class MenuButtonAnim : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        Animator myAnim;
        Vector3 originalScale;

        private void Awake()
        {
            myAnim = GetComponent<Animator>();
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            myAnim.SetBool("hover", true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            myAnim.SetBool("hover", false);
        }
    }
}