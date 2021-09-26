using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXGrayScaleRenderer), PostProcessEvent.AfterStack, "Custom/PostEffects/Grayscale")]
public sealed class PEFXGrayScale : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
}
public sealed class PEFXGrayScaleRenderer : PostProcessEffectRenderer<PEFXGrayScale>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/Grayscale"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}