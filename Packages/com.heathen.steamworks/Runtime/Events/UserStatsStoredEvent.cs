#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    /// <summary>
    /// A custom serializable <see cref="UnityEvent{T0}"/> which handles <see cref="UserStatsStored_t"/> data.
    /// </summary>
    [Serializable]
    public class UserStatsStoredEvent : UnityEvent<UserStatsStored_t>
    { }
}
#endif
