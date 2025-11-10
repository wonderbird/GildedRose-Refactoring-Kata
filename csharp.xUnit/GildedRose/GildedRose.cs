using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";

    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    private bool IsAgedBrie(Item item) => item.Name == AgedBrie;
    
    private bool IsBackstagePass(Item item) => item.Name == BackstagePasses;
    
    private bool IsSulfuras(Item item) => item.Name == Sulfuras;
    
    private bool IsAtMaxQuality(Item item) => item.Quality >= 50;
    
    private bool IsAtMinQuality(Item item) => item.Quality <= 0;

    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            
            // Sulfuras never changes
            if (IsSulfuras(item))
            {
                continue;
            }

            if (!IsAgedBrie(item) && !IsBackstagePass(item))
            {
                if (!IsAtMinQuality(item))
                {
                    item.Quality = item.Quality - 1;
                }

                item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0)
                {
                    if (!IsAtMinQuality(item))
                    {
                        item.Quality = item.Quality - 1;
                    }
                }
            }
            else
            {
                if (!IsAtMaxQuality(item))
                {
                    item.Quality = item.Quality + 1;

                    if (IsBackstagePass(item))
                    {
                        if (item.SellIn < 11 && !IsAtMaxQuality(item))
                        {
                            item.Quality = item.Quality + 1;
                        }

                        if (item.SellIn < 6 && !IsAtMaxQuality(item))
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }

                item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0)
                {
                    if (IsAgedBrie(item))
                    {
                        if (!IsAtMaxQuality(item))
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                    else if (IsBackstagePass(item))
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }
            }
        }
    }
}