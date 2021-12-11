using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXFogRenderer), PostProcessEvent.AfterStack, "Custom/PostEffects/Fog")]
public sealed class PEFXFog : PostProcessEffectSettings
{
    public ColorParameter DistColor = new ColorParameter { value = Color.black };

    [Range(1f, 0f), Tooltip("Distance Fog Effect")]
    public FloatParameter DistEffect = new FloatParameter { value = 0.5f };

    [Range(0f, 3f), Tooltip("Exponential Control")]
    public FloatParameter ExponentialCtrl = new FloatParameter { value = 0.5f };

    [Range(0f, 3f), Tooltip("Exponential Control")]
    public FloatParameter StartOffset = new FloatParameter { value = 0.5f };

}
public sealed class PEFXFogRenderer : PostProcessEffectRenderer<PEFXFog>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/Fog"));
        sheet.properties.SetVector("_DistanceColorGeneral", settings.DistColor);
        sheet.properties.SetFloat("_DistanceFogEffectGeneral", settings.DistEffect);
        sheet.properties.SetFloat("_KneeGeneral", settings.ExponentialCtrl);
        sheet.properties.SetFloat("_StartOffsetGeneral", settings.StartOffset);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}