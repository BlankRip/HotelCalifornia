using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Shaders
{
    [ExecuteAlways]
    public class CameraMatrix : MonoBehaviour
    {
        [SerializeField] Material ghostRepelMaterial;

        void Start()
        {
            GetComponent<Camera>().depthTextureMode |= DepthTextureMode.DepthNormals;
        }

        void OnPreRender()
        {
            ghostRepelMaterial.SetMatrix("_CamToWorld", GetComponent<Camera>().cameraToWorldMatrix);
        }
    }
}