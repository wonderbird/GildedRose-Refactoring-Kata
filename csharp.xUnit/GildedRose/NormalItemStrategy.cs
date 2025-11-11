namespace GildedRoseKata;

/// <summary>
/// Update strategy for normal items.
/// Quality degrades by 1 per day before sell-by date, and by 2 per day after.
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

