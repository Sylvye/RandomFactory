using System.Collections.Generic;

public enum ItemType
{
    Resource,
    Tool,
    Tile,
    Misc
}

public static class ItemTypeHelpers
{
    public static List<ItemType> AllItemTypes() =>
        new List<ItemType>
        {
            ItemType.Resource,
            ItemType.Tool,
            ItemType.Tile,
            ItemType.Misc
        };
}