using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using RimWorld;
using RimWorld.Planet;
using Verse;
using UnityEngine;
using Verse.AI;
using Verse.Sound;

namespace ForcedMarch
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("ari.Rimworld.ForcedMarch");
            Log.Message("Harmony patched");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(Caravan), nameof(Caravan.GetGizmos))]
    public static class GetGizmosPatch
    {
        [HarmonyPostfix]
        public static IEnumerable<Gizmo> GetGizmosPostfix(IEnumerable<Gizmo> __result, Caravan __instance)
        {
            foreach (Gizmo gizmo in __result)
            {
                yield return gizmo;
            }

            yield return ForcedMarchUtility.ForcedMarchCommand(__instance);
        }
    }

    [HarmonyPatch(typeof(Caravan))]
    [HarmonyPatch("NightResting", MethodType.Getter)]
    public static class CaravanNightRestUtilityPatch
    {
        [HarmonyPrefix]
        public static bool CaravanNightRestingPrefix(ref bool __result, Caravan __instance)
        {
            if (ForcedMarchUtility.GetCaravanMarch(__instance))
            {
                return false;
            }

            __result = false;
            return true;
        }
    }
}