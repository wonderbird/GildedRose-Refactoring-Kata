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

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (!IsAgedBrie(Items[i]) && !IsBackstagePasses(Items[i]))
            {
                if (Items[i].Quality > MIN_QUALITY)
                {
                    if (!IsSulfuras(Items[i]))
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (IsQualityBelowMax(Items[i]))
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (IsBackstagePasses(Items[i]))
                    {
                        if (Items[i].SellIn < BACKSTAGE_TIER2_THRESHOLD)
                        {
                            if (IsQualityBelowMax(Items[i]))
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < BACKSTAGE_TIER3_THRESHOLD)
                        {
                            if (IsQualityBelowMax(Items[i]))
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                Items[i].SellIn = Items[i].SellIn - 1;
            }

            if (Items[i].SellIn < SELL_BY_DATE)
            {
                if (!IsAgedBrie(Items[i]))
                {
                    if (!IsBackstagePasses(Items[i]))
                    {
                        if (Items[i].Quality > MIN_QUALITY)
                        {
                            if (!IsSulfuras(Items[i]))
                            {
                                Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = MIN_QUALITY;
                    }
                }
                else
                {
                    if (IsQualityBelowMax(Items[i]))
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }
                }
            }
        }
    }
}