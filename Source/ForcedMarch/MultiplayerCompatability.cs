using Multiplayer.API;
using Verse;

namespace ForcedMarch
{

    [StaticConstructorOnStartup]
    static class MultiplayerCompatibility
    {
        static MultiplayerCompatibility()
        {
            if (!MP.enabled) return;
            MP.RegisterSyncMethod(typeof(ForcedMarchUtility), nameof(ForcedMarchUtility.FlipCaravanMarch));
            MP.RegisterAll();
        }
        
    }
}