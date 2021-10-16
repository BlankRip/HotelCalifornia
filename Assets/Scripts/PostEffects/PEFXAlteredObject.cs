using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXAlteredObjectRenderer), PostProcessEvent.AfterStack, "Custom/PostEffects/AltererObject")]
public sealed class PEFXAlteredObject : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };

    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter effect = new FloatParameter { value = 0.5f };

    [Range(0f, 10f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter mult = new FloatParameter { value = 0.5f };

    [Range(0f, 10f), Tooltip("Grayscale effect intensity.")]
    public IntParameter iteration = new IntParameter { value = 1 };

    [Range(0f, 10f), Tooltip("Grayscale effect intensity.")]
    public ColorParameter effectColor = new ColorParameter { value = Color.white };

    [Tooltip("Distortion Texture")]
    public TextureParameter distortionTexture = new TextureParameter() { value = null };

    [Range(0f, 10f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter distortionScale = new FloatParameter { value = 0.5f };

    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter distortionEffect = new FloatParameter { value = 0.5f };

    [Range(0f, 10f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter distortionSpeed = new FloatParameter { value = 0.5f };
}
public sealed class PEFXAlteredObjectRenderer : PostProcessEffectRenderer<PEFXAlteredObject>
{

    RenderTexture rt1, rt2, rt3, rt4;
    Texture tempTex;
    // bloom extract meatrial

    public override void Init()
    {
        base.Init();
        rt1 = RenderTexture.GetTemporary(Screen.width / 10, Screen.height / 10);
        rt2 = RenderTexture.GetTemporary(Screen.width / 10, Screen.height / 10);
        rt3 = RenderTexture.GetTemporary(Screen.width, Screen.height);
        rt4 = RenderTexture.GetTemporary(Screen.width, Screen.height);
    }

    public override void Render(PostProcessRenderContext context)
    {
        tempTex = Shader.GetGlobalTexture("_PossesedTex");

        if (tempTex)
        {
            var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/AltererObject"));
            context.command.Blit(tempTex, rt1);
            var boxBlur = context.propertySheets.Get(Shader.Find("Custom/PostEffects/BoxBlur"));
            boxBlur.properties.SetFloat("_Shift", settings.effect);
            boxBlur.properties.SetFloat("_Mult", settings.mult);

            for (int i = 0; i < settings.iteration; i++)
            {
                // box blur
                context.command.BlitFullscreenTriangle(rt1, rt2, boxBlur, 0);
                context.command.Blit(rt2, rt1);
            }

            var smokeUp = context.propertySheets.Get(Shader.Find("Custom/PostEffects/SmokeUp"));
            smokeUp.properties.SetTexture("_DistortionTex", settings.distortionTexture.value != null ? settings.distortionTexture.value : RuntimeUtilities.blackTexture);
            smokeUp.properties.SetFloat("_DistScale", settings.distortionScale);
            smokeUp.properties.SetFloat("_DistEffect", settings.distortionEffect);
            smokeUp.properties.SetFloat("_DistSpeed", settings.distortionSpeed);
            smokeUp.properties.SetTexture("_TexB", tempTex != null ? tempTex : RuntimeUtilities.blackTexture);
            context.command.BlitFullscreenTriangle(rt1, rt2, smokeUp, 0);

            var maskTex = context.propertySheets.Get(Shader.Find("Custom/PostEffects/Substract"));
            maskTex.properties.SetTexture("_TexB", tempTex != null ? tempTex : RuntimeUtilities.blackTexture);
            context.command.BlitFullscreenTriangle(rt2, rt3, maskTex, 0);

            sheet.properties.SetTexture("_PossesedTex", rt3);
            sheet.properties.SetVector("_Color", settings.effectColor);
            context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
        }
        else
        {
            context.command.BlitFullscreenTriangle(context.source, context.destination);
        }
    }
}