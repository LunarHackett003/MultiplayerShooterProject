#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    /// <summary>
    /// Unity Event for <see cref="ActiveBeaconsUpdated_t"/> data
    /// </summary>
    [System.Serializable]
    public class ActiveBeaconsUpdatedEvent : UnityEvent<ActiveBeaconsUpdated_t> { }
}
#endif