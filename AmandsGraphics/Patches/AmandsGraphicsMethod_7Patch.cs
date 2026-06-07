using System.Reflection;
using SPT.Reflection.Patching;

namespace AmandsGraphics.Patches;

public class AmandsGraphicsMethod_7Patch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        //Todo; Check, likely incorrect (3.10 -> 4.0)
        return typeof(EffectsController).GetMethod("method_7", BindingFlags.Instance | BindingFlags.Public);
    }

    [PatchPostfix]
    public static void PatchPostFix(ref EffectsController __instance)
    {
        if (AmandsGraphicsClass.fastBlur != null && AmandsGraphicsPlugin.HealthEffectHit.Value == EEnabledFeature.On)
        {
            AmandsGraphicsClass.fastBlur.enabled = false;
        }
    }
}
