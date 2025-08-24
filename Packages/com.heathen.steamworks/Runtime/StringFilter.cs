#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public struct StringFilter
    {
        public string key;
        public string value;
        public ELobbyComparison comparison;
    }
}
#endif