namespace GildedRoseKata;

/// <summary>
/// Strategy for Aged Brie.
/// Quality increases by 1 per day before sell-by date, by 2 after.
/// Quality never exceeds 50.
/// </summary>
public class AgedBrieStrategy : BaseUpdateStrategy
{
    public override void UpdateItem(Item item)
    {
        IncreaseQuality(item);

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            IncreaseQuality(item);
        }
    }
}

