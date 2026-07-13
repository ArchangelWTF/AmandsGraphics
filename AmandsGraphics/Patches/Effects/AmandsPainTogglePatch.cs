using System.Reflection;
using AmandsGraphics.Enums;
using SPT.Reflection.Patching;

namespace AmandsGraphics.Patches.Effects;

public sealed class AmandsPainTogglePatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        //EffectsController.CC_RadialBlurAccumulator in 4.1
        return typeof(EffectsController.Class637).GetMethod(nameof(EffectsController.Class637.Toggle));
    }

    [PatchPrefix]
    public static bool PatchPreFix(ref object __instance, ref bool value)
    {
        if (AmandsGraphicsPlugin.HealthEffectPain.Value == EEnabledFeature.On)
        {
            value = false;
        }

        return true;
    }
}
