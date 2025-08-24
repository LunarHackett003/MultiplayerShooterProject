#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
    [RequireComponent(typeof(TMPro.TextMeshProUGUI))]
    public class SetAchievementDescription : MonoBehaviour
    {
        public string apiName;
        private TMPro.TextMeshProUGUI description;

        private void Start()
        {
            description = GetComponent<TMPro.TextMeshProUGUI>();
            if (description != null)
            {
                if (API.App.Initialized)
                {
                    if (!string.IsNullOrEmpty(apiName))
                        description.text = API.StatsAndAchievements.Client.GetAchievementDisplayAttribute(apiName, AchievementAttributes.desc);
                }
                else
                    API.App.evtSteamInitialized.AddListener(Refresh);
            }
        }

        private void Refresh()
        {
            if ((description != null))
            {
                if (API.App.Initialized)
                {
                    if (!string.IsNullOrEmpty(apiName))
                        description.text = API.StatsAndAchievements.Client.GetAchievementDisplayAttribute(apiName, AchievementAttributes.desc);
                }
                API.App.evtSteamInitialized.RemoveListener(Refresh);
            }
        }
    }
}
#endif