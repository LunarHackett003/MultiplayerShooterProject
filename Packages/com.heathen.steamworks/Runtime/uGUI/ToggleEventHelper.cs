#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration.UI
{
    public class ToggleEventHelper : MonoBehaviour
    {
        public UnityEvent on;
        public UnityEvent off;

        public void ToggleChanged(bool value)
        {
            if (value)
                on.Invoke();
            else
                off.Invoke();
        }
    }
}
#endif