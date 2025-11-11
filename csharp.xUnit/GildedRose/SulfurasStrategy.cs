namespace GildedRoseKata;

/// <summary>
/// Update strategy for Sulfuras, the legendary item.
/// Quality and SellIn never change (no-op implementation).
/// </summary>
public class SulfurasStrategy : IUpdateStrategy
{
    public void UpdateItem(Item item)
    {
        // Sulfuras is a legendary item that never changes
        // No-op implementation
    }
}

