namespace GildedRoseKata;

public class BackstagePassesStrategy : ItemUpdateStrategyBase
{
    private const int BACKSTAGE_TIER2_THRESHOLD = 11;
    private const int BACKSTAGE_TIER3_THRESHOLD = 6;

    private int GetBackstagePassesQualityIncrease(Item item)
    {
        if (item.SellIn < BACKSTAGE_TIER3_THRESHOLD)
        {
            return 3;
        }
        if (item.SellIn < BACKSTAGE_TIER2_THRESHOLD)
        {
            return 2;
        }
        return 1;
    }

    public override void UpdateQuality(Item item)
    {
        if (IsQualityBelowMax(item))
        {
            int increaseAmount = GetBackstagePassesQualityIncrease(item);
            int maxIncrease = MAX_QUALITY - item.Quality;
            int actualIncrease = increaseAmount < maxIncrease ? increaseAmount : maxIncrease;
            IncreaseQuality(item, actualIncrease);
        }

        UpdateSellIn(item);

        if (IsPastSellByDate(item))
        {
            ResetQualityToZero(item);
        }
    }
}

