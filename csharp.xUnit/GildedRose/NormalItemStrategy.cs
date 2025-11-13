namespace GildedRoseKata;

public class NormalItemStrategy : ItemUpdateStrategyBase
{
    public override void UpdateQuality(Item item)
    {
        if (IsQualityAboveMin(item))
        {
            DecreaseQuality(item);
        }

        UpdateSellIn(item);

        if (IsPastSellByDate(item))
        {
            if (IsQualityAboveMin(item))
            {
                DecreaseQuality(item);
            }
        }
    }
}

