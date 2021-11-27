using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXGhostVissionRenderer), PostProcessEvent.AfterStack, "Custom/PostEffects/GhostVission")]
public sealed class PEFXGhostVission : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("scan vector distance")]
    public FloatParameter stepDist = new FloatParameter { value = 0.5f };

    [Range(0f, 3f), Tooltip("Out Value")]
    public FloatParameter outValueControl = new FloatParameter { value = 0.5f };

    [Range(0f, 3f), Tooltip("Expo Knee")]
    public FloatParameter expoKnee = new FloatParameter { value = 0.5f };

    [Range(0f, 5f), Tooltip("Output Strength")]
    public FloatParameter outStrength = new FloatParameter { value = 0.5f };

    [Tooltip("Step Count")]
    public FloatParameter stepCount = new FloatParameter { value = 0.5f };

    public ColorParameter ghostColor = new ColorParameter { value = Color.black };

    public ColorParameter fogColor = new ColorParameter { value = Color.black };

    [Range(0f, 5f), Tooltip("Color Tightness")]
    public FloatParameter fogDistance = new FloatParameter { value = 0.5f };

    [Range(0f, 5f), Tooltip("Color Tightness")]
    public FloatParameter colorTightness = new FloatParameter { value = 0.5f };

    public TextureParameter distortionTexture = new TextureParameter {value = null};
    [Range(0f, 5f), Tooltip("Distortion Texture Size")]
    public FloatParameter dstortionSize = new FloatParameter { value = 0.5f };

    [Range(0f, 20f), Tooltip("Distortion Speed")]
    public FloatParameter dstortionSpeed = new FloatParameter { value = 0.5f };

    [Range(0f, 20f), Tooltip("Distortion Strength")]
    public FloatParameter dstortionSrength = new FloatParameter { value = 0.5f };

    [Range(0f, 5f), Tooltip("Distortion Softness")]
    public FloatParameter dstortionSoftness = new FloatParameter { value = 0.5f };


   //  [Tooltip("Step Count")]
   // public VectorParameter bloomColor = new VectorParameter { value = 0.5f };
}
public sealed class PEFXGhostVissionRenderer : PostProcessEffectRenderer<PEFXGhostVission>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/GhostVission"));
        sheet.properties.SetFloat("_StepDist", settings.stepDist);
        sheet.properties.SetFloat("_ValueControl", settings.outValueControl);
        sheet.properties.SetFloat("_OutKnee", settings.expoKnee);
        sheet.properties.SetFloat("_OutStrength", settings.outStrength);
        sheet.properties.SetFloat("_StepCount", settings.stepCount);
        sheet.properties.SetVector("_GhostZoneColor", settings.ghostColor);
        sheet.properties.SetVector("_FogColor", settings.fogColor);
        sheet.properties.SetTexture("_DistortionTex", settings.distortionTexture);
        sheet.properties.SetFloat("_DistortionScale", settings.dstortionSize);
        sheet.properties.SetFloat("_DistortionSpeed", settings.dstortionSpeed);
        sheet.properties.SetFloat("_DistortionStrength", settings.dstortionSrength);
        sheet.properties.SetFloat("_DistortionSSoftness", settings.dstortionSoftness);
        sheet.properties.SetFloat("_ColorTightness", settings.colorTightness);
        sheet.properties.SetFloat("_FogDistance", settings.fogDistance);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}