#if !DISABLESTEAMWORKS && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using Steamworks;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Heathen.SteamworksIntegration
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> containing the definition of a Steamworks Achievement.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that this object simply contains the definition of an achievement that has been created in the Steamworks API.
    /// for more information please see <a href="https://partner.steamgames.com/doc/features/achievements">https://partner.steamgames.com/doc/features/achievements</a>
    /// </para>
    /// </remarks>
    [HelpURL("https://kb.heathen.group/steamworks/features/achievements")]
    [CreateAssetMenu(menuName = "Steamworks/Achievement Object")]
    public class AchievementObject : ScriptableObject
    {
        /// <summary>
        /// The API Name of the Achievement
        /// </summary>
        public string ApiName
        {
            get => data;
            set => data = value;
        }
        /// <summary>
        /// The display name of the achievement as seen by the user
        /// </summary>
        public string Name => data.Name;
        /// <summary>
        /// THe description of the achievement as seen by the user
        /// </summary>
        public string Description => data.Description;
        /// <summary>
        /// The percentage of users who have unlocked this achievement
        /// </summary>
        public float GlobalPercent => data.GlobalPercent;
        /// <summary>
        /// Is the achievement hidden from the user
        /// </summary>
        public bool Hidden => data.Hidden;
        /// <summary>
        /// The API Name as it appears in the Steamworks portal.
        /// </summary>
        [SerializeField]
        private AchievementData data;

        /// <summary>
        /// Indicates that this achievement has been unlocked by this user.
        /// </summary>
        /// <remarks>
        /// Only available on client builds
        /// </remarks>
        public bool IsAchieved
        {
            get => data.IsAchieved;
            set => data.IsAchieved = value;
        }
        /// <summary>
        /// Indicates the time the achievement was unlocked if at all
        /// </summary>
        public DateTime? UnlockTime => data.UnlockTime;

        /// <summary>
        /// <para>Unlocks the achievement.</para>
        /// </summary>
        public void Unlock() => IsAchieved = true;

        /// <summary>
        /// <para>Resets the unlock status of an achievement.</para>
        /// </summary>
        public void ClearAchievement() => IsAchieved = false;

        /// <summary>
        /// Unlock the achievement for the <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// Only available on server builds
        /// </remarks>
        /// <param name="user">The user to unlock the achievement for</param>
        public void Unlock(UserData user) => data.Unlock(user);

        /// <summary>
        /// Clears the achievement for the <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// Only available on server builds
        /// </remarks>
        /// <param name="user">The user to clear for</param>
        public void ClearAchievement(UserData user) => data.ClearAchievement(user);

        /// <summary>
        /// Gets the achievement status for the <paramref name="user"/>
        /// </summary>
        /// <remarks>
        /// Only available on server builds
        /// </remarks>
        /// <param name="user">The user to read the status for</param>
        /// <returns>The status of the achievement</returns>
        public bool GetAchievementStatus(UserData user) => data.GetAchievementStatus(user);
        /// <summary>
        /// Get the unlock state and time for this achievement for a specific user.
        /// </summary>
        /// <param name="user">The user to check</param>
        /// <returns>(<see cref="bool"/> unlocked, <see cref="DataTime"/> unlockTime) indicating the state and time of the achievement for the indicated user if known.</returns>
        public (bool unlocked, DateTime unlockTime) GetAchievementAndUnlockTime(UserData user) => data.GetAchievementAndUnlockTime(user);
        /// <summary>
        /// Gets the icon for this achievement as seen by the logged in user, this will return either the locked or unlocked icon depending on the state of the achievement for this user
        /// </summary>
        /// <param name="callback">A delegate of the form (<see cref="Texture2D"/> result) that is invoked when the process completes</param>
        public void GetIcon(Action<Texture2D> callback) => API.StatsAndAchievements.Client.GetAchievementIcon(data, callback);
        /// <summary>
        /// Request Steam client store the current state of all stats and achievements
        /// </summary>
        public void Store() => API.StatsAndAchievements.Client.StoreStats();

        /// <summary>
        /// This will create a ScriptableObject based on this achievement ... in general you should not need this
        /// The AchievementData struct has all the same features of the ScriptableObject but is much lighter weight and
        /// more suitable for creation at runtime.
        /// </summary>
        /// <returns>Creates a new ScriptableObject of type <see cref="AchievementObject"/> and returns it</returns>
        public static AchievementObject CreateScriptableObject(string apiName)
        {
            var newObject = UnityEngine.ScriptableObject.CreateInstance<AchievementObject>();
            newObject.ApiName = apiName;
            return newObject;
        }
    }
}
#endif