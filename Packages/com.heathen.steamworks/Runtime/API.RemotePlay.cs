#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using UnityEngine;

namespace Heathen.SteamworksIntegration.API
{
    /// <summary>
    /// Functions that provide information about Steam Remote Play sessions, streaming your game content to another computer or to a Steam Link app or hardware.
    /// </summary>
    public static class RemotePlay
    {
        public static class Client
        {
            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
            static void Init()
            {
                eventSteamRemotePlaySessionConnected = new SteamRemotePlaySessionConnectedEvent();
                eventSteamRemotePlaySessionDisconnected = new SteamRemotePlaySessionDisconnectedEvent();
                m_SteamRemotePlaySessionConnected_t = null;
                m_SteamRemotePlaySessionDisconnected_t = null;
            }

            /// <summary>
            /// Invoked when a session connects
            /// </summary>
            public static SteamRemotePlaySessionConnectedEvent EventSessionConnected
            {
                get
                {
                    if (m_SteamRemotePlaySessionConnected_t == null)
                        m_SteamRemotePlaySessionConnected_t = Callback<SteamRemotePlaySessionConnected_t>.Create(eventSteamRemotePlaySessionConnected.Invoke);

                    return eventSteamRemotePlaySessionConnected;
                }
            }
            /// <summary>
            /// Invoked when a session disconnects
            /// </summary>
            public static SteamRemotePlaySessionDisconnectedEvent EventSessionDisconnected
            {
                get
                {
                    if (m_SteamRemotePlaySessionDisconnected_t == null)
                        m_SteamRemotePlaySessionDisconnected_t = Callback<SteamRemotePlaySessionDisconnected_t>.Create(eventSteamRemotePlaySessionDisconnected.Invoke);

                    return eventSteamRemotePlaySessionDisconnected;
                }
            }

            private static SteamRemotePlaySessionConnectedEvent eventSteamRemotePlaySessionConnected = new SteamRemotePlaySessionConnectedEvent();
            private static SteamRemotePlaySessionDisconnectedEvent eventSteamRemotePlaySessionDisconnected = new SteamRemotePlaySessionDisconnectedEvent();

            private static Callback<SteamRemotePlaySessionConnected_t> m_SteamRemotePlaySessionConnected_t;
            private static Callback<SteamRemotePlaySessionDisconnected_t> m_SteamRemotePlaySessionDisconnected_t;

            /// <summary>
            /// Get the number of currently connected Steam Remote Play sessions
            /// </summary>
            /// <returns></returns>
            public static uint GetSessionCount() => SteamRemotePlay.GetSessionCount();
            /// <summary>
            /// Get the currently connected Steam Remote Play session ID at the specified index
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public static RemotePlaySessionID_t GetSessionID(int index) => SteamRemotePlay.GetSessionID(index);
            /// <summary>
            /// Get the collection of current remote play sessions
            /// </summary>
            /// <returns></returns>
            public static RemotePlaySessionID_t[] GetSessions()
            {
                var count = SteamRemotePlay.GetSessionCount();
                var results = new RemotePlaySessionID_t[count];
                for (int i = 0; i < count; i++)
                {
                    results[i] = SteamRemotePlay.GetSessionID(i);
                }
                return results;
            }
            /// <summary>
            /// Get the UserData of the connected user
            /// </summary>
            /// <param name="session"></param>
            /// <returns></returns>
            public static UserData GetSessionUser(RemotePlaySessionID_t session) => SteamRemotePlay.GetSessionSteamID(session);
            /// <summary>
            /// Get the name of the session client device
            /// </summary>
            /// <param name="session"></param>
            /// <returns></returns>
            public static string GetSessionClientName(RemotePlaySessionID_t session) => SteamRemotePlay.GetSessionClientName(session);
            /// <summary>
            /// Get the form factor of the session client device
            /// </summary>
            /// <param name="session"></param>
            /// <returns></returns>
            public static ESteamDeviceFormFactor GetSessionClientFormFactor(RemotePlaySessionID_t session) => SteamRemotePlay.GetSessionClientFormFactor(session);
            /// <summary>
            /// Get the resolution, in pixels, of the session client device. This is set to 0x0 if the resolution is not available.
            /// </summary>
            /// <param name="session"></param>
            /// <returns></returns>
            public static Vector2Int GetSessionClientResolution(RemotePlaySessionID_t session)
            {
                SteamRemotePlay.BGetSessionClientResolution(session, out int x, out int y);
                return new Vector2Int(x, y);
            }
            /// <summary>
            /// Invite a friend to join the game using Remote Play Together
            /// </summary>
            /// <param name="user"></param>
            /// <returns></returns>
            public static bool SendInvite(UserData user) => SteamRemotePlay.BSendRemotePlayTogetherInvite(user);

#if STEAM_162
            /// <summary>
            /// <para> Make mouse and keyboard input for Remote Play Together sessions available via GetInput() instead of being merged with local input</para>
            /// </summary>
            public static bool EnableRemotePlayTogetherDirectInput() => SteamRemotePlay.BEnableRemotePlayTogetherDirectInput();

            /// <summary>
            /// <para> Merge Remote Play Together mouse and keyboard input with local input</para>
            /// </summary>
            public static void DisableRemotePlayTogetherDirectInput() => SteamRemotePlay.DisableRemotePlayTogetherDirectInput();

            /// <summary>
            /// <para> Get input events from Remote Play Together sessions</para>
            /// <para> This is available after calling BEnableRemotePlayTogetherDirectInput()</para>
            /// <para> pInput is an array of input events that will be filled in by this function, up to unMaxEvents.</para>
            /// <para> This returns the number of events copied to pInput, or the number of events available if pInput is nullptr.</para>
            /// </summary>
            public static uint GetInput(RemotePlayInput_t[] Input, uint MaxEvents) => SteamRemotePlay.GetInput(Input, MaxEvents);

            /// <summary>
            /// <para> Set the mouse cursor visibility for a remote player</para>
            /// <para> This is available after calling BEnableRemotePlayTogetherDirectInput()</para>
            /// </summary>
            public static void SetMouseVisibility(RemotePlaySessionID_t unSessionID, bool bVisible) => SteamRemotePlay.SetMouseVisibility(unSessionID, bVisible);

            /// <summary>
            /// <para> Set the mouse cursor position for a remote player</para>
            /// <para> This is available after calling BEnableRemotePlayTogetherDirectInput()</para>
            /// <para> This is used to warp the cursor to a specific location and isn't needed during normal event processing.</para>
            /// <para> The position is normalized relative to the window, where 0,0 is the upper left, and 1,1 is the lower right.</para>
            /// </summary>
            public static void SetMousePosition(RemotePlaySessionID_t SessionID, float Normalized_X, float Normalized_Y) => SteamRemotePlay.SetMousePosition(SessionID, Normalized_X, Normalized_Y);

            //TODO: Need to update cursor pointer
            /// <summary>
            /// <para> Create a cursor that can be used with SetMouseCursor()</para>
            /// <para> This is available after calling BEnableRemotePlayTogetherDirectInput()</para>
            /// <para> Parameters:</para>
            /// <para> nWidth - The width of the cursor, in pixels</para>
            /// <para> nHeight - The height of the cursor, in pixels</para>
            /// <para> nHotX - The X coordinate of the cursor hot spot in pixels, offset from the left of the cursor</para>
            /// <para> nHotY - The Y coordinate of the cursor hot spot in pixels, offset from the top of the cursor</para>
            /// <para> pBGRA - A pointer to the cursor pixels, with the color channels in red, green, blue, alpha order</para>
            /// <para> nPitch - The distance between pixel rows in bytes, defaults to nWidth * 4</para>
            /// </summary>
            //public static RemotePlayCursorID_t CreateMouseCursor(int nWidth, int nHeight, int nHotX, int nHotY, IntPtr pBGRA, int nPitch = 0) => SteamRemotePlay.CreateMouseCursor(nWidth, nHeight, nHotX, nHotY, pBGRA, nPitch);

            /// <summary>
            /// <para> Set the mouse cursor for a remote player</para>
            /// <para> This is available after calling BEnableRemotePlayTogetherDirectInput()</para>
            /// <para> The cursor ID is a value returned by CreateMouseCursor()</para>
            /// </summary>
            public static void SetMouseCursor(RemotePlaySessionID_t SessionID, RemotePlayCursorID_t CursorID) => SteamRemotePlay.SetMouseCursor(SessionID, CursorID);
#endif
        }
    }
}
#endif