using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    private const int MAX_QUALITY = 50;
    private const int MIN_QUALITY = 0;
    private const int BACKSTAGE_PASS_FIRST_THRESHOLD = 11;
    private const int BACKSTAGE_PASS_SECOND_THRESHOLD = 6;
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
            else if (Items[i].Name == AGED_BRIE)
            {
                UpdateAgedBrie(Items[i]);
            }
            else if (Items[i].Name == BACKSTAGE_PASSES)
            {
                UpdateBackstagePass(Items[i]);
            }
            else
            {
                UpdateNormalItem(Items[i]);
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

    private void DecrementSellIn(Item item)
    {
        item.SellIn = item.SellIn - 1;
    }

    private void UpdateNormalItem(Item item)
    {
        DecreaseQuality(item, 1);
        item.SellIn = item.SellIn - 1;
        if (item.SellIn < 0)
        {
            DecreaseQuality(item, 1);
        }
    }

    private void UpdateAgedBrie(Item item)
    {
        IncreaseQuality(item, 1);
        item.SellIn = item.SellIn - 1;
        if (item.SellIn < 0)
        {
            IncreaseQuality(item, 1);
        }
    }

    private void UpdateBackstagePass(Item item)
    {
        IncreaseQuality(item, 1);
        if (item.SellIn < BACKSTAGE_PASS_FIRST_THRESHOLD)
        {
            IncreaseQuality(item, 1);
        }
        if (item.SellIn < BACKSTAGE_PASS_SECOND_THRESHOLD)
        {
            IncreaseQuality(item, 1);
        }
        item.SellIn = item.SellIn - 1;
        if (item.SellIn < 0)
        {
            item.Quality = MIN_QUALITY;
        }
    }
}