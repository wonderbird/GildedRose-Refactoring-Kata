namespace GildedRoseKata;

public static class UpdaterFactory
{
    public static IItemUpdater GetUpdater(Item item)
    {
        if (item.Name == "Aged Brie")
        {
            return new AgedBrieUpdater();
        }
        if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
        {
            return new BackstagePassUpdater();
        }
        if (item.Name == "Sulfuras, Hand of Ragnaros")
        {
            return new SulfurasUpdater();
        }
        return new RegularItemUpdater();
    }
}

