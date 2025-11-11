namespace GildedRoseKata;

/// <summary>
/// Strategy for Aged Brie.
/// Quality increases by 1 per day before sell-by date, by 2 after.
/// Quality never exceeds 50.
/// </summary>
public class AgedBrieStrategy : IUpdateStrategy
{
    private const int MaxQuality = 50;

    public void UpdateItem(Item item)
    {
        IncreaseQuality(item);

        DecrementSellIn(item);

        if (IsPastSellByDate(item))
        {
            IncreaseQuality(item);
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

