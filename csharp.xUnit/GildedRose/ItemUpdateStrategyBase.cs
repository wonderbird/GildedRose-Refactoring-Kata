namespace GildedRoseKata;

public abstract class ItemUpdateStrategyBase : IItemUpdateStrategy
{
    protected const int MAX_QUALITY = 50;
    protected const int MIN_QUALITY = 0;
    protected const int SELL_BY_DATE = 0;

    protected bool IsQualityBelowMax(Item item)
    {
        return item.Quality < MAX_QUALITY;
    }

    protected bool IsQualityAboveMin(Item item)
    {
        return item.Quality > MIN_QUALITY;
    }

    protected void DecreaseQuality(Item item, int amount = 1)
    {
        item.Quality = item.Quality - amount;
    }

    protected void IncreaseQuality(Item item, int amount = 1)
    {
        item.Quality = item.Quality + amount;
    }

    protected void ResetQualityToZero(Item item)
    {
        item.Quality = MIN_QUALITY;
    }

    protected void UpdateSellIn(Item item)
    {
        item.SellIn = item.SellIn - 1;
    }

    protected bool IsPastSellByDate(Item item)
    {
        return item.SellIn < SELL_BY_DATE;
    }

    public abstract void UpdateQuality(Item item);
}

