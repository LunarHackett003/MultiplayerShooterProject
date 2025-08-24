#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    [System.Serializable]
    public class LobbyDataEvent : UnityEvent<LobbyData> { }

    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }
}
#endif