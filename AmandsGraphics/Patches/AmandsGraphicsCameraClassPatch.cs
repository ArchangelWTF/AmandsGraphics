using System.Reflection;
using SPT.Reflection.Patching;

namespace AmandsGraphics.Patches;

public sealed class AmandsGraphicsCameraClassPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod()
    {
        return typeof(CameraClass).GetMethod(nameof(CameraClass.Blur));
    }

    [PatchPrefix]
    public static bool PatchPrefix(ref CameraClass __instance, bool isActive, float time)
    {
        AmandsGraphicsClass.CameraClassBlur = isActive;

        if (!isActive && __instance.IsActive)
        {
            return true;
        }

        return AmandsGraphicsPlugin.UIDepthOfField.Value == EUIDepthOfField.Off;
    }
}
