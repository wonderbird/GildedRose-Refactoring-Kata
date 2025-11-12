namespace GildedRoseKata;

/// <summary>
/// Strategy for updating Aged Brie: increases quality by 1, decrements sell-in,
/// and increases quality by 1 again if past sell-by date.
/// </summary>
public class AgedBrieStrategy : BaseItemUpdateStrategy
{
    /// <summary>
    /// Updates Aged Brie: increases quality by 1, decrements sell-in,
    /// and increases quality by 1 again if past sell-by date.
    /// </summary>
    /// <param name="item">The Aged Brie item to update.</param>
    public override void Update(Item item)
    {
        IncreaseQuality(item, 1);
        DecrementSellIn(item);
        if (IsPastSellByDate(item))
        {
            IncreaseQuality(item, 1);
        }
    }
}

