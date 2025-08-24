#if !DISABLESTEAMWORKS  && (STEAMWORKSNET || STEAM_LEGACY || STEAM_161 || STEAM_162)
using System;
using System.Linq;

namespace Heathen.SteamworksIntegration
{
    [Serializable]
    public struct ItemChangeRecord
    {
        public ItemDefinitionObject item;
        public ItemInstanceChangeRecord[] changes;

        public bool HasChanges => changes != null && changes.Length > 0;
        public long TotalQuantityBefore => changes.Sum(x => x.quantityBefore);
        public long TotalQuantityAfter => changes.Sum(x => x.quantityAfter);
        public long TotalQuantityChange => changes.Sum(x => x.QuantityChange);
    }
}
#endif