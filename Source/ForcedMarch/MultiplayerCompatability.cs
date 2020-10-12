using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using HarmonyLib;
using Multiplayer.API;
using RimWorld.Planet;
using Verse;

namespace ForcedMarch
{

    [StaticConstructorOnStartup]
    static class MultiplayerCompatibility
    {
        private static Type marchingUtility;
        private static FieldInfo marchingField;
        static MultiplayerCompatibility()
        {
            if (!MP.enabled) return;
            marchingUtility = AccessTools.TypeByName("ForcedMarch.ForcedMarchUtility");
            marchingField = AccessTools.Field(marchingUtility, "caravan_marches");

            MP.RegisterSyncMethod(typeof(ForcedMarchUtility), nameof(ForcedMarchUtility.FlipCaravanMarch));
            MP.RegisterAll();
        }
        
    }
}