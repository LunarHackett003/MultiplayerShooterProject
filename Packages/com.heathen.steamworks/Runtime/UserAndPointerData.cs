#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System;
using UnityEngine.EventSystems;
#if ENABLE_INPUT_SYSTEM
#endif

namespace Heathen.SteamworksIntegration.UI
{
    [Serializable]
    public class UserAndPointerData
    {
        public UserData user;
        public PointerEventData pointerEventData;

        public UserAndPointerData(UserData userData, PointerEventData data)
        {
            user = userData;
            pointerEventData = data;
        }
    }
}
#endif