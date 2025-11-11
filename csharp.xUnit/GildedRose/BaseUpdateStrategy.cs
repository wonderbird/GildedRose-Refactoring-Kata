namespace GildedRoseKata;

/// <summary>
/// Base class for update strategies providing shared constants and helper methods.
/// </summary>
public abstract class BaseUpdateStrategy : IUpdateStrategy
{
    protected const int MaxQuality = 50;
    protected const int MinQuality = 0;

    public abstract void UpdateItem(Item item);

    protected bool IsPastSellByDate(Item item) => item.SellIn < 0;

    protected bool IsAtMaxQuality(Item item) => item.Quality >= MaxQuality;

    protected bool IsAtMinQuality(Item item) => item.Quality <= MinQuality;

    protected void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }

    protected void IncreaseQuality(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality++;
        }
    }

    protected void DecreaseQuality(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality--;
        }
    }
}

