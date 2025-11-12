namespace GildedRoseKata;

/// <summary>
/// Strategy for updating Backstage passes: increases quality based on sell-in thresholds,
/// decrements sell-in, and sets quality to 0 if past concert date.
/// </summary>
public class BackstagePassStrategy : BaseItemUpdateStrategy
{
    // Backstage pass thresholds
    private const int BACKSTAGE_PASS_FIRST_THRESHOLD = 11;
    private const int BACKSTAGE_PASS_SECOND_THRESHOLD = 6;

    /// <summary>
    /// Updates Backstage passes: increases quality based on sell-in thresholds,
    /// decrements sell-in, and sets quality to 0 if past concert date.
    /// </summary>
    /// <param name="item">The Backstage pass item to update.</param>
    public override void Update(Item item)
    {
        int increment = CalculateBackstagePassIncrement(item.SellIn);
        IncreaseQuality(item, increment);
        DecrementSellIn(item);
        if (IsPastSellByDate(item))
        {
            item.Quality = MIN_QUALITY;
        }
    }

    /// <summary>
    /// Calculates the quality increment amount for Backstage passes based on sell-in thresholds.
    /// </summary>
    /// <param name="sellIn">The sell-in value of the item.</param>
    /// <returns>The amount by which to increase quality (1, 2, or 3).</returns>
    private int CalculateBackstagePassIncrement(int sellIn)
    {
        int increment = 1;
        if (sellIn < BACKSTAGE_PASS_FIRST_THRESHOLD)
        {
            increment++;
        }
        if (sellIn < BACKSTAGE_PASS_SECOND_THRESHOLD)
        {
            increment++;
        }
        return increment;
    }
}

