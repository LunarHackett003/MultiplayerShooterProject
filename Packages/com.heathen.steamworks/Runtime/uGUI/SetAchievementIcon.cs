#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System;
using UnityEngine;

namespace Heathen.SteamworksIntegration.UI
{
    [RequireComponent(typeof(UnityEngine.UI.RawImage))]
    public class SetAchievementIcon : MonoBehaviour
    {
        public string apiName;
        private UnityEngine.UI.RawImage image;

        private void Start()
        {
            image = GetComponent<UnityEngine.UI.RawImage>();
            API.StatsAndAchievements.Client.EventAchievementStatusChanged.AddListener(HandleChange);

            if (!string.IsNullOrEmpty(apiName))
            {
                if (API.App.Initialized)
                {
                    Refresh();
                }
                else
                    API.App.evtSteamInitialized.AddListener(Refresh);
            }
        }

        private void OnDestroy()
        {
            API.StatsAndAchievements.Client.EventAchievementStatusChanged.RemoveListener(HandleChange);
        }

        private void HandleChange(string arg0, bool arg1)
        {
            Refresh();
        }

        private void Refresh()
        {
            if (!string.IsNullOrEmpty(apiName))
            {
                API.StatsAndAchievements.Client.GetAchievementIcon(apiName, texture =>
                {
                    image.texture = texture;
                });
            }

            API.App.evtSteamInitialized.RemoveListener(Refresh);
        }
    }
}
#endif