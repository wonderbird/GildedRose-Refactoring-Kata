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

    private void UpdateNormalItem(Item item)
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

    private void UpdateAgedBrie(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality = item.Quality + 1;
        }

        item.SellIn = item.SellIn - 1;

        if (item.SellIn < 0)
        {
            if (!IsAtMaxQuality(item))
            {
                item.Quality = item.Quality + 1;
            }
        }
    }

    private void UpdateBackstagePass(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality = item.Quality + 1;

            if (item.SellIn < 11 && !IsAtMaxQuality(item))
            {
                item.Quality = item.Quality + 1;
            }

            if (item.SellIn < 6 && !IsAtMaxQuality(item))
            {
                item.Quality = item.Quality + 1;
            }
        }

        item.SellIn = item.SellIn - 1;

        if (item.SellIn < 0)
        {
            item.Quality = item.Quality - item.Quality;
        }
    }

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
                UpdateNormalItem(item);
            }
            else
            {
                if (IsAgedBrie(item))
                {
                    UpdateAgedBrie(item);
                }
                else // IsBackstagePass
                {
                    UpdateBackstagePass(item);
                }
            }
        }
    }
}