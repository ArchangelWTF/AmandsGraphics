using System.Reflection;
using AmandsGraphics.Enums;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine;

namespace AmandsGraphics.Patches.Effects;

public sealed class AmandsPainkillerAddEffectPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return AmandsGraphicsPlugin.PainKillerEffectType.GetMethod("AddEffect", BindingFlags.Instance | BindingFlags.Public);
    }

    [PatchPostfix]
    public static void PatchPostFix(ref object __instance)
    {
        if (AmandsGraphicsPlugin.HealthEffectPainkiller.Value == EEnabledFeature.On)
        {
            List<IEffect> ActiveEffects = Traverse.Create(__instance).Field("ActiveEffects").GetValue<List<IEffect>>();
            if (ActiveEffects != null)
            {
                /*bool bool_1 = Traverse.Create(__instance).Field("bool_1").GetValue<bool>();
                float float_2 = Traverse.Create(__instance).Field("float_2").GetValue<float>();*/

                float maxEffectValue;
                if (ActiveEffects.Count <= 0)
                {
                    maxEffectValue = 0f;
                }
                else
                {
                    maxEffectValue = Mathf.Min(1.0f * AmandsGraphicsPlugin.PainkillerSaturation.Value, 1f);
                }
                Traverse.Create(__instance).Field("MaxEffectValue").SetValue(maxEffectValue);
                /*if (bool_1)
                {
                    Traverse.Create(__instance).Field("float_3").SetValue((ActiveEffects.Count > 0) ? 0.015f * AmandsGraphicsPlugin.PainkillerCAIntensity.Value : float_2);
                }*/
            }
        }
    }
}
