namespace GildedRoseKata;

public class AgedBrieStrategy : ItemUpdateStrategyBase
{
    public override void UpdateQuality(Item item)
    {
        if (IsQualityBelowMax(item))
        {
            IncreaseQuality(item);
        }

        UpdateSellIn(item);

        if (IsPastSellByDate(item))
        {
            if (IsQualityBelowMax(item))
            {
                IncreaseQuality(item);
            }
        }
    }
}

