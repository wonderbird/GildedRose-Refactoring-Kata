using System.Collections.Generic;

namespace csharp
{
    public static class ProductNames
    {
        public const string Conjured = "Conjured";
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    }

    public class ProductAttributes
    {
        public static string Summoned = "Summoned";
    }

    public class GildedRose
    {
        public const int BackstagePassesThresholdDoubleQuality = 11;
        public const int BackstagePassesThresholdTripleQuality = 6;
        public const int SellInDecreaseStep = 1;
        private readonly IList<Item> Items;

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items) UpdateItemQuality(item);
        }

        private static void UpdateItemQuality(Item item)
        {
            UpdateItemQualitySellInNotExpired(item);

            ReduceSellInDays(item);

            UpdateItemQualityForExpiredItems(item);
        }

        private static void UpdateItemQualitySellInNotExpired(Item item)
        {
            if (item.Name == ProductNames.Sulfuras)
                return;

            if (item.Name != ProductNames.AgedBrie && item.Name != ProductNames.BackstagePasses)
                item.DecreaseQuality();
            else
                item.IncreaseQuality();

            if (item.Name == ProductNames.BackstagePasses)
            {
                if (item.SellIn < BackstagePassesThresholdDoubleQuality) item.IncreaseQuality();

                if (item.SellIn < BackstagePassesThresholdTripleQuality) item.IncreaseQuality();
            }
        }

        private static void UpdateItemQualityForExpiredItems(Item item)
        {
            if (item.SellIn >= 0) return;

            if (item.Name == ProductNames.AgedBrie)
                item.IncreaseQuality();
            else if (item.Name == ProductNames.BackstagePasses)
                item.Quality = Item.MinQuality;
            else if (item.Name != ProductNames.Sulfuras) item.DecreaseQuality();
        }

        private static void ReduceSellInDays(Item item)
        {
            if (item.Name != ProductNames.Sulfuras) item.SellIn -= SellInDecreaseStep;
        }
    }
}