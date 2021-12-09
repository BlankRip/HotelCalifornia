using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knotgames.Shaders
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

        [Header("General Surface Settings")]
        [SerializeField] Color generalRimColor;
        [SerializeField] [Range(0, 20)] float generalRimEffect;

        bool updatValues;

        void Start()
        {
            updatValues = true;
        }

        public void Update()
        {
            if (updatValues)
            {
                Shader.SetGlobalTexture("_LightColorGradient", lightRamp);
                Shader.SetGlobalColor("_CharRimColor", charRimColor);
                Shader.SetGlobalFloat("_CharRimPower", charRimEffect);
                Shader.SetGlobalFloat("_CharGlosiness", charSurfaceGloss);
                Shader.SetGlobalFloat("_CharGlossPower", charSurfaceGlossPower);
                Shader.SetGlobalFloat("_CharColorBoost", charSurfaceColorBoost);
                Shader.SetGlobalColor("_CharEmissionColor", charEmissionColor);
                Shader.SetGlobalColor("_RimColor", generalRimColor);
                Shader.SetGlobalFloat("_RimPower", generalRimEffect);

                updatValues = false;
            }
        }

        public void UpdateValues()
        {
            updatValues = true;
        }

    }
}