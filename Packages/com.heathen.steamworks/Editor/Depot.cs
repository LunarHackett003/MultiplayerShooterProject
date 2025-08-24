#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using UnityEditor;

namespace Heathen.SteamworksIntegration.Editors
{
    [System.Serializable]
    public class Depot
    {
        public string name;
        public uint id;
        public BuildTarget target;
        public string extension;
    }
}
#endif