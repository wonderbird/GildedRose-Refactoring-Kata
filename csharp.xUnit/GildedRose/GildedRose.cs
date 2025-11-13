using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";

    private const int MAX_QUALITY = 50;
    private const int MIN_QUALITY = 0;
    private const int BACKSTAGE_TIER2_THRESHOLD = 11;
    private const int BACKSTAGE_TIER3_THRESHOLD = 6;
    private const int SELL_BY_DATE = 0;

    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    private bool IsAgedBrie(Item item)
    {
        return item.Name == AGED_BRIE;
    }

    private bool IsBackstagePasses(Item item)
    {
        return item.Name == BACKSTAGE_PASSES;
    }

    private bool IsSulfuras(Item item)
    {
        return item.Name == SULFURAS;
    }

    private bool IsNormalItem(Item item)
    {
        return !IsAgedBrie(item) && !IsBackstagePasses(item);
    }

    private bool IsPastSellByDate(Item item)
    {
        return item.SellIn < SELL_BY_DATE;
    }

    private bool IsQualityBelowMax(Item item)
    {
        return item.Quality < MAX_QUALITY;
    }

    private bool IsQualityAboveMin(Item item)
    {
        return item.Quality > MIN_QUALITY;
    }

    private void DecreaseQuality(Item item, int amount = 1)
    {
        item.Quality = item.Quality - amount;
    }

    private void IncreaseQuality(Item item, int amount = 1)
    {
        item.Quality = item.Quality + amount;
    }

    private void ResetQualityToZero(Item item)
    {
        item.Quality = MIN_QUALITY;
    }

    private void UpdateSellIn(Item item)
    {
        item.SellIn = item.SellIn - 1;
    }

    private void UpdateNormalItemQuality(Item item)
    {
        if (IsQualityAboveMin(item))
        {
            DecreaseQuality(item);
        }
    }

    private void UpdateAgedBrieQuality(Item item)
    {
        if (IsQualityBelowMax(item))
        {
            IncreaseQuality(item);
        }
    }

    private int GetBackstagePassesQualityIncrease(Item item)
    {
        if (item.SellIn < BACKSTAGE_TIER3_THRESHOLD)
        {
            return 3;
        }
        if (item.SellIn < BACKSTAGE_TIER2_THRESHOLD)
        {
            return 2;
        }
        return 1;
    }

    private void UpdateBackstagePassesQuality(Item item)
    {
        if (IsQualityBelowMax(item))
        {
            int increaseAmount = GetBackstagePassesQualityIncrease(item);
            int maxIncrease = MAX_QUALITY - item.Quality;
            int actualIncrease = increaseAmount < maxIncrease ? increaseAmount : maxIncrease;
            IncreaseQuality(item, actualIncrease);
        }
    }

    private void ApplyPastSellByDateEffects(Item item)
    {
        if (!IsAgedBrie(item))
        {
            if (IsNormalItem(item))
            {
                if (IsQualityAboveMin(item))
                {
                    DecreaseQuality(item);
                }
            }
            else
            {
                ResetQualityToZero(item);
            }
        }
        else
        {
            if (IsQualityBelowMax(item))
            {
                IncreaseQuality(item);
            }
        }
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (IsSulfuras(Items[i]))
            {
                continue;
            }

            if (IsNormalItem(Items[i]))
            {
                UpdateNormalItemQuality(Items[i]);
            }
            else
            {
                if (IsAgedBrie(Items[i]))
                {
                    UpdateAgedBrieQuality(Items[i]);
                }
                else if (IsBackstagePasses(Items[i]))
                {
                    UpdateBackstagePassesQuality(Items[i]);
                }
            }

            UpdateSellIn(Items[i]);

            if (IsPastSellByDate(Items[i]))
            {
                ApplyPastSellByDateEffects(Items[i]);
            }
        }
    }
}