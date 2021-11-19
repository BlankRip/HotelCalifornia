using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bearroll;

namespace Knotgames.Extensions
{
    public class GDOCRuntimeOccludee : MonoBehaviour
    {
        private void Awake()
        {
            GDOC.AddOrUpdateOccludee(gameObject);
        }
    }
}