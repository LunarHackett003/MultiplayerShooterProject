#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public struct LobbyInvite : IEquatable<LobbyInvite>, IComparable<LobbyInvite>
    {
        public LobbyInvite_t data;

        public readonly UserData FromUser => data.m_ulSteamIDUser;
        public readonly LobbyData ToLobby => data.m_ulSteamIDLobby;
        public readonly GameData ForGame => data.m_ulGameID;

        public static implicit operator LobbyInvite(LobbyInvite_t native) => new LobbyInvite { data = native };
        public static implicit operator LobbyInvite_t(LobbyInvite heathen) => heathen.data;

        public readonly bool Equals(LobbyInvite other) =>
            data.m_ulSteamIDLobby == other.data.m_ulSteamIDLobby &&
            data.m_ulSteamIDUser == other.data.m_ulSteamIDUser &&
            data.m_ulGameID == other.data.m_ulGameID;

        public override readonly bool Equals(object obj) => obj is LobbyInvite other && Equals(other);

        public override readonly int GetHashCode() =>
            HashCode.Combine(data.m_ulSteamIDLobby, data.m_ulSteamIDUser, data.m_ulGameID);

        public int CompareTo(LobbyInvite other) =>
            data.m_ulSteamIDLobby.CompareTo(other.data.m_ulSteamIDLobby);

        public static bool operator ==(LobbyInvite left, LobbyInvite right) => left.Equals(right);
        public static bool operator !=(LobbyInvite left, LobbyInvite right) => !left.Equals(right);
        public static bool operator <(LobbyInvite left, LobbyInvite right) => left.CompareTo(right) < 0;
        public static bool operator >(LobbyInvite left, LobbyInvite right) => left.CompareTo(right) > 0;
        public static bool operator <=(LobbyInvite left, LobbyInvite right) => left.CompareTo(right) <= 0;
        public static bool operator >=(LobbyInvite left, LobbyInvite right) => left.CompareTo(right) >= 0;

        public override string ToString() =>
            $"LobbyInvite from {data.m_ulSteamIDUser} to {data.m_ulSteamIDLobby} for {data.m_ulGameID}";
    }
}
#endif