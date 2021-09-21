using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[System.Serializable]
[PostProcess(typeof(PEFXRadialBlurRenderer), PostProcessEvent.AfterStack, "PEFX/RadialBloom")]
public sealed class PEFXRadialBlur : PostProcessEffectSettings
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

   //  [Tooltip("Step Count")]
   // public VectorParameter bloomColor = new VectorParameter { value = 0.5f };
}
public sealed class PEFXRadialBlurRenderer : PostProcessEffectRenderer<PEFXRadialBlur>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("PEFX/RadialBloom"));
        sheet.properties.SetFloat("_StepDist", settings.stepDist);
        sheet.properties.SetFloat("_ValueControl", settings.outValueControl);
        sheet.properties.SetFloat("_OutKnee", settings.expoKnee);
        sheet.properties.SetFloat("_OutStrength", settings.outStrength);
        sheet.properties.SetFloat("_StepCount", settings.stepCount);
      //  sheet.properties.SetVector("_BloomColor", settings.bloomColor);

        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}