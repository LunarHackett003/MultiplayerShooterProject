#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    [System.Serializable]
    public class LobbyResponseEvent : UnityEvent<Steamworks.EChatRoomEnterResponse> { }

    [System.Serializable]
    public class EResultEvent : UnityEvent<Steamworks.EResult> { }
}
#endif