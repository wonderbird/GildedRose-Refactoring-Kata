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

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (!IsAgedBrie(Items[i]) && !IsBackstagePasses(Items[i]))
            {
                if (Items[i].Quality > MIN_QUALITY)
                {
                    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (Items[i].Quality < MAX_QUALITY)
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (IsBackstagePasses(Items[i]))
                    {
                        if (Items[i].SellIn < BACKSTAGE_TIER2_THRESHOLD)
                        {
                            if (Items[i].Quality < MAX_QUALITY)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < BACKSTAGE_TIER3_THRESHOLD)
                        {
                            if (Items[i].Quality < MAX_QUALITY)
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
                            if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                            {
                                Items[i].Quality = Items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
                else
                {
                    if (Items[i].Quality < MAX_QUALITY)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }
                }
            }
        }
    }
}