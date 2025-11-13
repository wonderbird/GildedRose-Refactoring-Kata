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

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (!IsAgedBrie(Items[i]) && !IsBackstagePasses(Items[i]))
            {
                if (IsQualityAboveMin(Items[i]))
                {
                    if (!IsSulfuras(Items[i]))
                    {
                        DecreaseQuality(Items[i]);
                    }
                }
            }
            else
            {
                if (IsQualityBelowMax(Items[i]))
                {
                    IncreaseQuality(Items[i]);

                    if (IsBackstagePasses(Items[i]))
                    {
                        if (Items[i].SellIn < BACKSTAGE_TIER2_THRESHOLD)
                        {
                            if (IsQualityBelowMax(Items[i]))
                            {
                                IncreaseQuality(Items[i]);
                            }
                        }

                        if (Items[i].SellIn < BACKSTAGE_TIER3_THRESHOLD)
                        {
                            if (IsQualityBelowMax(Items[i]))
                            {
                                IncreaseQuality(Items[i]);
                            }
                        }
                    }
                }
            }

            if (!IsSulfuras(Items[i]))
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < SELL_BY_DATE)
            {
                if (!IsAgedBrie(Items[i]))
                {
                    if (!IsBackstagePasses(Items[i]))
                    {
                        if (IsQualityAboveMin(Items[i]))
                        {
                            if (!IsSulfuras(Items[i]))
                            {
                                DecreaseQuality(Items[i]);
                            }
                        }
                    }
                    else
                    {
                        ResetQualityToZero(Items[i]);
                    }
                }
                else
                {
                    if (IsQualityBelowMax(Items[i]))
                    {
                        IncreaseQuality(Items[i]);
                    }
                }
            }
        }
    }
}