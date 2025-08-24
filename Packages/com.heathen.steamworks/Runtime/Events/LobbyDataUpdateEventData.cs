#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public struct LobbyDataUpdateEventData
    {
        public LobbyData lobby;
        public LobbyMemberData? member;

        public static implicit operator LobbyDataUpdateEventData(LobbyDataUpdate_t c)
        {
            if (c.m_ulSteamIDLobby != c.m_ulSteamIDMember)
                return new LobbyDataUpdateEventData()
                {
                    lobby = c.m_ulSteamIDLobby,
                    member = new LobbyMemberData { lobby = c.m_ulSteamIDLobby, user = c.m_ulSteamIDMember },
                };
            else
                return new LobbyDataUpdateEventData()
                {
                    lobby = c.m_ulSteamIDLobby,
                    member = null
                };
        }
    }
}
#endif