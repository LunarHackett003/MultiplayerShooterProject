#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)

namespace Heathen.SteamworksIntegration
{
    public enum SampleRateMethod
    {
        Optimal,
        Native,
        Custom
    }
}
#endif