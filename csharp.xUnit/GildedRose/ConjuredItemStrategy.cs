namespace GildedRoseKata;

/// <summary>
/// Update strategy for Conjured items.
/// Quality degrades twice as fast as normal items:
/// by 2 per day before sell-by date, and by 4 per day after.
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

