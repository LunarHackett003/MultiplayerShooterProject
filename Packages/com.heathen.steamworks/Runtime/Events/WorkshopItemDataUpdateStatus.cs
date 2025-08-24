#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;

namespace Heathen.SteamworksIntegration
{
    public struct WorkshopItemDataUpdateStatus
    {
        public bool hasError;
        public string errorMessage;
        public WorkshopItemData data;
        public SubmitItemUpdateResult_t? submitItemUpdateResult;
    }
}
#endif