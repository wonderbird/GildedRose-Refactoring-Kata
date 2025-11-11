namespace GildedRoseKata;

/// <summary>
/// Strategy for Backstage passes.
/// Quality increases as concert approaches:
/// - +1 per day when SellIn > 10
/// - +2 per day when SellIn is 6-10
/// - +3 per day when SellIn is 1-5
/// Quality drops to 0 after concert (SellIn ≤ 0).
/// Quality never exceeds 50 before concert.
/// </summary>
public class BackstagePassStrategy : BaseUpdateStrategy
{
    private const int BackstageFirstTierBoundary = 11;
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

