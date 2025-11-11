namespace GildedRoseKata;

/// <summary>
/// Strategy for Conjured items.
/// Quality decreases by 2 per day before sell-by date (twice as fast as normal).
/// Quality decreases by 4 per day after sell-by date (twice as fast as normal).
/// Quality never goes below 0.
/// </summary>
public class ConjuredItemStrategy : BaseUpdateStrategy
{
    public override void UpdateItem(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality = System.Math.Max(MinQuality, item.Quality - 2);
        }

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            if (!IsAtMinQuality(item))
            {
                item.Quality = System.Math.Max(MinQuality, item.Quality - 2);
            }
        }
    }
}

