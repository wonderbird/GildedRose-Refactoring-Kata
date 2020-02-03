using System.Collections.Generic;

namespace csharp
{
    public static class ProductNames
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    }

    public class GildedRose
    {
        public const int MaxQuality = 50;
        public const int BackstagePassesThresholdDoubleQuality = 11;
        public const int BackstagePassesThresholdTripleQuality = 6;
        public const int MinQuality = 0;
        public const int QualityDecreaseStep = 1;
        public const int QualityIncreaseStep = 1;
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
                DecreaseQuality(item);
            else
                IncreaseQuality(item);

            if (item.Name == ProductNames.BackstagePasses)
            {
                if (item.SellIn < BackstagePassesThresholdDoubleQuality)
                    IncreaseQuality(item);

                if (item.SellIn < BackstagePassesThresholdTripleQuality)
                    IncreaseQuality(item);
            }
        }

        private static void UpdateItemQualityForExpiredItems(Item item)
        {
            if (item.SellIn >= 0) return;

            if (item.Name == ProductNames.AgedBrie)
                IncreaseQuality(item);
            else if (item.Name == ProductNames.BackstagePasses)
                item.Quality = MinQuality;
            else if (item.Name != ProductNames.Sulfuras)
                DecreaseQuality(item);
        }

        private static void ReduceSellInDays(Item item)
        {
            if (item.Name != ProductNames.Sulfuras) item.SellIn -= SellInDecreaseStep;
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < MaxQuality) item.Quality += QualityIncreaseStep;
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > MinQuality) item.Quality -= QualityDecreaseStep;
        }
    }
}