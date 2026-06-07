using System.Reflection;
using HarmonyLib;
using SPT.Reflection.Patching;

namespace AmandsGraphics.Patches.Effects;

public sealed class AmandsEffectsControllerPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(EffectsController).GetMethod(nameof(EffectsController.Awake));
    }

    [PatchPostfix]
    private static void PatchPostFix(ref EffectsController __instance)
    {
        if (AmandsGraphicsPlugin.PainKillerEffectType == null || AmandsGraphicsPlugin.PainEffectType == null)
        {
            object EffectsList = Traverse.Create(__instance).Field("list_0").GetValue<object>();
            object[] EffectsListItems = Traverse.Create(EffectsList).Field("_items").GetValue<object[]>();
            if (EffectsListItems != null)
            {
                foreach (object Effect in EffectsListItems)
                {
                    if (AmandsGraphicsPlugin.PainKillerEffectType == null && Traverse.Create(Effect).Field("float_4").FieldExists())
                    {
                        CC_Sharpen cc_Sharpen_0 = Traverse.Create(Effect).Field("cc_Sharpen_0").GetValue<CC_Sharpen>();
                        if (cc_Sharpen_0 != null)
                        {
                            AmandsGraphicsPlugin.PainKillerEffectType = Effect.GetType();
                            new AmandsPainkillerAddEffectPatch().Enable();
                            new AmandsPainkillerDeleteEffectPatch().Enable();
                            continue;
                        }
                    }
                    if (AmandsGraphicsPlugin.PainEffectType == null && Traverse.Create(Effect).Field("cc_RadialBlur_0").FieldExists())
                    {
                        AmandsGraphicsPlugin.PainEffectType = Effect.GetType();
                        new AmandsPainTogglePatch().Enable();
                        continue;
                    }
                }
            }
        }
    }
}
