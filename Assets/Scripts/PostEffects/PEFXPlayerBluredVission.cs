using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(VissionBlurRenderer), PostProcessEvent.AfterStack, "Custom/VissionBlur")]
public sealed class VissionBlur : PostProcessEffectSettings
{
    [Range(0f, 0.01f), Tooltip("Blur intensity.")]
    public FloatParameter effect = new FloatParameter { value = 0.5f };

    [Range(0f, 20f), Tooltip("Blur Count.")]
    public IntParameter iterationCount = new IntParameter { value = 2 };

    [Tooltip("Lookup Texture")]
    public TextureParameter lookupTexture = new TextureParameter(){ value = null };

    [Tooltip("Smoke Texture")]
    public TextureParameter ghostSmokeTexture = new TextureParameter(){ value = null };

    [Tooltip("Smoke Texture")]
    public TextureParameter distortionTexture = new TextureParameter(){ value = null };

    [Range(0f, 3f), Tooltip("Vapour Strength")]
    public FloatParameter vapourStrength = new FloatParameter { value = 0.5f };

    [Tooltip("Vapour Color")]
    public ColorParameter vapourColor = new ColorParameter();

    [Range(0f, 1f), Tooltip("Select Image")]
    public FloatParameter selectImage = new FloatParameter { value = 0.5f };

}

public sealed class VissionBlurRenderer : PostProcessEffectRenderer<VissionBlur>
{

    RenderTexture rt1, rt2;
    // bloom extract meatrial

    public override void Init()
    {
        base.Init();
        rt1 = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4);
        rt2 = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4);


    }
    public override void Render(PostProcessRenderContext context)
    {

        // var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/AddTextures"));
        //   sheet.properties.SetFloat("_Blend", settings.effect);

        var extractor = context.propertySheets.Get(Shader.Find("Custom/PostEffects/CorruptedVisionColorShift"));
        extractor.properties.SetTexture("_LookupTexture", settings.lookupTexture.value != null ? settings.lookupTexture.value : RuntimeUtilities.blackTexture);
        context.command.BlitFullscreenTriangle(context.source, rt1, extractor, 0);

        for (int i = 0; i < settings.iterationCount; i++)
        {

            // blur vertical
            var blurVertical = context.propertySheets.Get(Shader.Find("Custom/PostEffects/BlurVertical"));
            blurVertical.properties.SetFloat("_Shift", settings.effect);
            context.command.BlitFullscreenTriangle(rt1, rt2, blurVertical, 0);

            // blur horizontal
            var blurHorizontal = context.propertySheets.Get(Shader.Find("Custom/PostEffects/BlurHorizontal"));
            blurHorizontal.properties.SetFloat("_Shift", settings.effect);
            context.command.BlitFullscreenTriangle(rt2, rt1, blurHorizontal, 0);

        }

        var imageCombiner = context.propertySheets.Get(Shader.Find("Custom/PostEffects/CorruptedVisionCombine"));
        imageCombiner.properties.SetTexture("_BlurImage", rt1);
        imageCombiner.properties.SetTexture("_GhostSmokeImage", settings.ghostSmokeTexture.value != null ? settings.ghostSmokeTexture.value : RuntimeUtilities.blackTexture);
        imageCombiner.properties.SetTexture("_DistortionTexture", settings.distortionTexture.value != null ? settings.distortionTexture.value : RuntimeUtilities.blackTexture);
        imageCombiner.properties.SetFloat("_SelectImage", settings.selectImage);
        imageCombiner.properties.SetVector("_VapourColor", settings.vapourColor);
        imageCombiner.properties.SetFloat("_VapourStrength", settings.vapourStrength);

        context.command.BlitFullscreenTriangle(context.source, context.destination, imageCombiner, 0);

    }
}