namespace GildedRoseKata;

/// <summary>
/// Strategy for normal items.
/// Quality decreases by 1 per day before sell-by date, by 2 after.
/// Quality never goes below 0.
/// </summary>
public class NormalItemStrategy : BaseUpdateStrategy
{
    public override void UpdateItem(Item item)
    {
        DecreaseQuality(item);

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            DecreaseQuality(item);
        }
    }
}

