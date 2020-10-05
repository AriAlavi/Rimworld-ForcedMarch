using Multiplayer.API;
using Verse;

namespace ForcedMarch
{
    class MultiplayerCompatability
    {
        [StaticConstructorOnStartup]
        static class MultiplayerCompatibility
        {
            static MultiplayerCompatibility()
            {
                 if (!MP.enabled) return;

                // MP.RegisterSyncMethod(typeof(ForcedMarchUtility), nameof(ForcedMarchUtility.ForcedMarchCommand));
                 MP.RegisterAll();
            }
        }
    }
}