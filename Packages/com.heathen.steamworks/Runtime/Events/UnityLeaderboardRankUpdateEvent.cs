#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public class UnityLeaderboardRankUpdateEvent : UnityEvent<LeaderboardEntry>
    { }
}
#endif
