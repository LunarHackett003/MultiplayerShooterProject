#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Toggle))]
    public class AchievementToggleHelper : MonoBehaviour
    {
        public string apiName;
        private UnityEngine.UI.Toggle toggle;

        private void Start()
        {
            toggle = GetComponent<UnityEngine.UI.Toggle>();
            if (API.StatsAndAchievements.Client.GetAchievement(apiName, out var value))
                toggle.SetIsOnWithoutNotify(value);

            toggle.onValueChanged.AddListener(HandleValueChanged);
        }

        private void HandleValueChanged(bool value)
        {
            if (value)
                API.StatsAndAchievements.Client.SetAchievement(apiName);
            else
                API.StatsAndAchievements.Client.ClearAchievement(apiName);
        }
    }
}
#endif