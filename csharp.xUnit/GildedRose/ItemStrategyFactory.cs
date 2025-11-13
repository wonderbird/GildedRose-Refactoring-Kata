namespace GildedRoseKata;

public static class ItemStrategyFactory
{
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";

    public static IItemUpdateStrategy GetStrategy(Item item)
    {
        if (item.Name == AGED_BRIE)
        {
            return new AgedBrieStrategy();
        }
        if (item.Name == BACKSTAGE_PASSES)
        {
            return new BackstagePassesStrategy();
        }
        if (item.Name == SULFURAS)
        {
            return new SulfurasStrategy();
        }
        return new NormalItemStrategy();
    }
}

