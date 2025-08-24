#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System;
using UnityEngine.Events;
#if ENABLE_INPUT_SYSTEM
#endif

namespace Heathen.SteamworksIntegration.UI
{
    [Serializable]
    public class UnityUserAndPointerDataEvent : UnityEvent<UserAndPointerData>
    { }
}
#endif