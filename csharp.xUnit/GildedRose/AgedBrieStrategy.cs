namespace GildedRoseKata;

/// <summary>
/// Update strategy for Aged Brie.
/// Quality improves by 1 per day before sell-by date, and by 2 per day after.
/// </summary>
public class AgedBrieStrategy : BaseUpdateStrategy
{
    public override void UpdateItem(Item item)
    {
        IncreaseQuality(item);

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            IncreaseQuality(item);
        }
    }
}

