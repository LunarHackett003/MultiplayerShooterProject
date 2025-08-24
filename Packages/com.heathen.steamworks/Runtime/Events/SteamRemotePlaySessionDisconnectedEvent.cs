#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    [System.Serializable]
    public class SteamRemotePlaySessionDisconnectedEvent : UnityEvent<SteamRemotePlaySessionDisconnected_t> { }
}
#endif