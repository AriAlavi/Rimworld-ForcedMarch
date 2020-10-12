using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace ForcedMarch
{
    public class ForcedMarchSettings : ModSettings
    {
        public override void ExposeData()
        {
            base.ExposeData();
        }
    }

    public class ForcedMarch : Mod
    {
        private ForcedMarchSettings settings;

        public ForcedMarch(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ForcedMarchSettings>();
            Log.Message("Forced March loaded");
        }

        public override string SettingsCategory()
        {
            return "ForcedMarch".Translate();
        }
    }

    [StaticConstructorOnStartup]
    public static class ForcedMarchUtility
    {
        public static Dictionary<Caravan, bool> caravan_marches = new Dictionary<Caravan, bool>();
        
        public static readonly Texture2D ForcedMarchingTex =
            ContentFinder<Texture2D>.Get("UI/Commands/Marching", true);
        public static readonly Texture2D NoMarchingTex =
            ContentFinder<Texture2D>.Get("UI/Commands/NotMarching", true);
        
        public static bool GetCaravanMarch(Caravan caravan)
        {
            if (!caravan_marches.ContainsKey(caravan))
            {
                return false;
            }
            if (caravan_marches[caravan])
            {
                return true;
            }

            return false;
        }

        public static void FlipCaravanMarch(Caravan caravan)
        {
            caravan_marches[caravan] = !GetCaravanMarch(caravan);
        }
 
        
        public static Command_Action ForcedMarchCommand(Caravan caravan)
        {
            bool currentState = GetCaravanMarch(caravan);
            Texture2D selectedIcon;
            if (currentState == true)
            {
                selectedIcon = ForcedMarchingTex;
            }
            else
            {
                selectedIcon = NoMarchingTex;
            }
            Command_Action commandAction = new Command_Action
            {
                defaultLabel = "ForcedMarch".Translate(),
                defaultDesc = "ForcedMarchDesc".Translate(),
                icon = selectedIcon,
                action = delegate()
                {
                    SoundStarter.PlayOneShotOnCamera(SoundDefOf.Tick_High, null);
                    FlipCaravanMarch(caravan);
                }
            };

            return commandAction;
        }

    }

}