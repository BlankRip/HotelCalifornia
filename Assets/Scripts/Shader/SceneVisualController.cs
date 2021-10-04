using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotagmes.Shaders
{
    [ExecuteAlways]
    public class SceneVisualController : MonoBehaviour
    {
        [Header("Light Settings")]
        [SerializeField] Texture2D lightRamp;

        [Header("Caharecter Surface Settings")]
        [SerializeField] Color charRimColor;
        [SerializeField] Color charEmissionColor;
        [SerializeField] [Range(0, 20)] float charRimEffect;
        [SerializeField] [Range(0, 20)] float charSurfaceGloss;
        [SerializeField] [Range(0, 20)] float charSurfaceGlossPower;
        [SerializeField] [Range(0, 3)] float charSurfaceColorBoost;

        void Start()
        {

        }

        public void Update()
        {
            Shader.SetGlobalTexture("_LightShadow", lightRamp);
            Shader.SetGlobalColor("_CharRimColor", charRimColor);
            Shader.SetGlobalFloat("_CharRimPower", charRimEffect);
            Shader.SetGlobalFloat("_CharGlosiness", charSurfaceGloss);
            Shader.SetGlobalFloat("_CharGlossPower", charSurfaceGlossPower);
            Shader.SetGlobalFloat("_CharColorBoost", charSurfaceColorBoost);
            Shader.SetGlobalColor("_CharEmissionColor", charEmissionColor);
            
        }
    }
}