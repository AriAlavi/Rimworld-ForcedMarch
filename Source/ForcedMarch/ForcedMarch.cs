using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Threading;
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


    public class March
    {
        public March(Caravan caravan)
        {
            
        }
    }
    
    [StaticConstructorOnStartup]
    public static class ForcedMarchUtility
    {
        public static Dictionary<Caravan, bool> caravan_marches;
        
        public static readonly Texture2D ForcedMarchingTex =
            ContentFinder<Texture2D>.Get("UI/Commands/Marching", true);
        public static readonly Texture2D NoMarchingTex =
            ContentFinder<Texture2D>.Get("UI/Commands/NotMarching", true);

        static ForcedMarchUtility()
        {
            caravan_marches = new Dictionary<Caravan, bool>();
        }

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
            bool current_state = GetCaravanMarch(caravan);
            Texture2D selected_icon;
            if (current_state == true)
            {
                selected_icon = ForcedMarchingTex;
            }
            else
            {
                selected_icon = NoMarchingTex;
            }
            Command_Action command_Action = new Command_Action
            {
                defaultLabel = "ForcedMarch".Translate(),
                defaultDesc = "ForcedMarchDesc".Translate(),
                icon = selected_icon,
                action = delegate()
                {
                    SoundStarter.PlayOneShotOnCamera(SoundDefOf.Tick_High, null);
                    FlipCaravanMarch(caravan);
                }
            };

            return command_Action;
        }

    }

}