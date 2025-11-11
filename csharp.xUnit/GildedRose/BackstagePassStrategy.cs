namespace GildedRoseKata;

/// <summary>
/// Update strategy for Backstage passes to concerts.
/// Quality increases as the concert approaches:
/// +1 per day when more than 10 days away,
/// +2 per day when 6-10 days away,
/// +3 per day when 1-5 days away,
/// drops to 0 immediately after the concert.
/// </summary>
public class BackstagePassStrategy : BaseUpdateStrategy
{
    /// <summary>Quality increases by 2 when SellIn drops below this value.</summary>
    private const int BackstageFirstTierBoundary = 11;
    
    /// <summary>Quality increases by 3 when SellIn drops below this value.</summary>
    private const int BackstageSecondTierBoundary = 6;

    public override void UpdateItem(Item item)
    {
        IncreaseQuality(item);

        if (item.SellIn < BackstageFirstTierBoundary)
        {
            IncreaseQuality(item);
        }

        if (item.SellIn < BackstageSecondTierBoundary)
        {
            IncreaseQuality(item);
        }

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            item.Quality = MinQuality;
        }
    }
}

