namespace GildedRoseKata;

/// <summary>
/// Strategy for Conjured items.
/// Quality decreases by 2 per day before sell-by date (twice as fast as normal).
/// Quality decreases by 4 per day after sell-by date (twice as fast as normal).
/// Quality never goes below 0.
/// </summary>
public class ConjuredItemStrategy : IUpdateStrategy
{
    private const int MinQuality = 0;

    public void UpdateItem(Item item)
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

    private bool IsPastSellByDate(Item item) => item.SellIn < 0;

    private bool IsAtMinQuality(Item item) => item.Quality <= MinQuality;

    private void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }
}

