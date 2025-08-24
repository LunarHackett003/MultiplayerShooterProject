#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public struct UserStatsReceived
    {
        public UserStatsReceived_t data;
        public GameData Id => data.m_nGameID;
        public EResult Result => data.m_eResult;
        public UserData User => data.m_steamIDUser;

        public static implicit operator UserStatsReceived(UserStatsReceived_t native) => new UserStatsReceived { data = native };
        public static implicit operator UserStatsReceived_t(UserStatsReceived heathen) => heathen.data;
    }
}
#endif