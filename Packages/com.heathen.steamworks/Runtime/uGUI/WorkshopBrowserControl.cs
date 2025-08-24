#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
#endif

namespace Heathen.SteamworksIntegration.UI
{
    public class WorkshopBrowserControl : MonoBehaviour
    {
        public GameObject itemTemplate;
    }
}
#endif