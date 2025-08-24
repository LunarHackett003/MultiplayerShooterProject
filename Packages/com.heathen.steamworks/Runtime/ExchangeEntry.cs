#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;

namespace Heathen.SteamworksIntegration
{
    public struct ExchangeEntry
    {
        public SteamItemInstanceID_t instance;
        public uint quantity;
    }
}
#endif