#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    /// <summary>
    /// Unity Event for <see cref="InputActionUpdate"/> data
    /// </summary>
    [System.Serializable]
    public class ActionUpdateEvent : UnityEvent<InputActionUpdate>
    { }
}
#endif