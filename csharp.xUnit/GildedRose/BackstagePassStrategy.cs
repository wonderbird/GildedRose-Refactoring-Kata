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
public class BackstagePassStrategy : IUpdateStrategy
{
    private const int MaxQuality = 50;
    private const int MinQuality = 0;
    private const int BackstageFirstTierBoundary = 11;
    private const int BackstageSecondTierBoundary = 6;

    public void UpdateItem(Item item)
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

    private bool IsPastSellByDate(Item item) => item.SellIn < 0;

    private bool IsAtMaxQuality(Item item) => item.Quality >= MaxQuality;

    private void IncreaseQuality(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality++;
        }
    }

    private void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }
}

