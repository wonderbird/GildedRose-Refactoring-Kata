namespace GildedRoseKata;

/// <summary>
/// Strategy for normal items.
/// Quality decreases by 1 per day before sell-by date, by 2 after.
/// Quality never goes below 0.
/// </summary>
public class NormalItemStrategy : IUpdateStrategy
{
    private const int MaxQuality = 50;
    private const int MinQuality = 0;

    public void UpdateItem(Item item)
    {
        DecreaseQuality(item);

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            DecreaseQuality(item);
        }
    }

    private bool IsPastSellByDate(Item item) => item.SellIn < 0;

    private bool IsAtMinQuality(Item item) => item.Quality <= MinQuality;

    private void DecreaseQuality(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality--;
        }
    }

    private void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }
}

