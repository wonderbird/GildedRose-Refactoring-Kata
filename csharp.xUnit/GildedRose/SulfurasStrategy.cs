namespace GildedRoseKata;

/// <summary>
/// Strategy for Sulfuras (legendary item).
/// Quality and SellIn never change - this is a no-op strategy.
/// </summary>
public class SulfurasStrategy : IUpdateStrategy
{
    public void UpdateItem(Item item)
    {
        // Sulfuras is a legendary item that never changes
        // No-op implementation
    }
}

