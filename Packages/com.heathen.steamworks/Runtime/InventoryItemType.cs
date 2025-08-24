#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)

namespace Heathen.SteamworksIntegration
{
    /// <summary>
    /// The type applied to a Steam Inventory Item
    /// </summary>
    public enum InventoryItemType
    {
        item,
        bundle,
        generator,
        playtimegenerator,
        tag_generator,
        tag_tool
    }
}
#endif