#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)

namespace Heathen.SteamworksIntegration
{
    public struct ConsumeOrder
    {
        public Steamworks.SteamItemDetails_t detail;
        public uint quantity;
    }
}
#endif