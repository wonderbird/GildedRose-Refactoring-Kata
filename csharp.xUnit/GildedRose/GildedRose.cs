using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    private const int MAX_QUALITY = 50;
    private const int MIN_QUALITY = 0;
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == SULFURAS)
            {
                continue;
            }

            if (Items[i].Name != AGED_BRIE && Items[i].Name != BACKSTAGE_PASSES)
            {
                if (Items[i].Quality > MIN_QUALITY)
                {
                    Items[i].Quality = Items[i].Quality - 1;
                }
            }
            else
            {
                if (Items[i].Quality < MAX_QUALITY)
                {
                    Items[i].Quality = Items[i].Quality + 1;

                    if (Items[i].Name == BACKSTAGE_PASSES)
                    {
                        if (Items[i].SellIn < 11)
                        {
                            if (Items[i].Quality < MAX_QUALITY)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }

                        if (Items[i].SellIn < 6)
                        {
                            if (Items[i].Quality < MAX_QUALITY)
                            {
                                Items[i].Quality = Items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            Items[i].SellIn = Items[i].SellIn - 1;

            if (Items[i].SellIn < 0)
            {
                if (Items[i].Name != AGED_BRIE)
                {
                    if (Items[i].Name != BACKSTAGE_PASSES)
                    {
                        if (Items[i].Quality > MIN_QUALITY)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
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

    private void DecreaseQuality(Item item, int amount)
    {
        if (item.Quality > MIN_QUALITY)
        {
            item.Quality = item.Quality - amount;
            if (item.Quality < MIN_QUALITY)
            {
                item.Quality = MIN_QUALITY;
            }
        }
    }

    private void IncreaseQuality(Item item, int amount)
    {
        if (item.Quality < MAX_QUALITY)
        {
            item.Quality = item.Quality + amount;
            if (item.Quality > MAX_QUALITY)
            {
                item.Quality = MAX_QUALITY;
            }
        }
    }
}