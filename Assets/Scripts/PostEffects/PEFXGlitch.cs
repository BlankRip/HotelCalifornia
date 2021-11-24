using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXGlitchRenderer), PostProcessEvent.AfterStack, "Custom/PostEffects/GlitchEffect")]
public sealed class PEFXGlitch : PostProcessEffectSettings
{
    [Range(0f, 50f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter ChromaShift = new FloatParameter { value = 0.5f };

    public ColorParameter DistColor = new ColorParameter { value = Color.black };

    [Range(1f, 0f), Tooltip("Distance Fog Effect")]
    public FloatParameter DistEffect = new FloatParameter { value = 0.5f };

}
public sealed class PEFXGlitchRenderer : PostProcessEffectRenderer<PEFXGlitch>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Custom/PostEffects/GlitchEffect"));
        sheet.properties.SetFloat("_ChromaShift", settings.ChromaShift);
        sheet.properties.SetVector("_DistanceColor", settings.DistColor);
        sheet.properties.SetFloat("_DistanceFogEffect", settings.DistEffect);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}