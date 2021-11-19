using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bearroll;

namespace Knotgames.Extensions
{
    public class GDOCRuntimeOccludee : MonoBehaviour
    {
        GDOC_Occludee myOccludee;
        private void Awake()
        {
            myOccludee = gameObject.AddComponent<GDOC_Occludee>();
            GDOC.AddOccludee(myOccludee);
        }
    }
}