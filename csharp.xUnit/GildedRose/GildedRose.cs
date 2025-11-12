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

    /// <summary>
    /// Updates the quality and sell-in values for all items in the inventory.
    /// Each item type follows specific rules for quality degradation or improvement.
    /// </summary>
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

    /// <summary>
    /// Decreases the quality of an item by the specified amount, ensuring it does not go below the minimum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be decreased.</param>
    /// <param name="amount">The amount by which to decrease the quality.</param>
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

    /// <summary>
    /// Increases the quality of an item by the specified amount, ensuring it does not exceed the maximum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be increased.</param>
    /// <param name="amount">The amount by which to increase the quality.</param>
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

    /// <summary>
    /// Updates a normal item: decreases quality by 1, decrements sell-in, and decreases quality by 1 again if past sell-by date.
    /// </summary>
    /// <param name="item">The normal item to update.</param>
    private void UpdateNormalItem(Item item)
    {
        DecreaseQuality(item, 1);
        DecrementSellIn(item);
        if (item.SellIn < 0)
        {
            DecreaseQuality(item, 1);
        }
    }

    private void UpdateAgedBrie(Item item)
    {
        IncreaseQuality(item, 1);
        DecrementSellIn(item);
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
        DecrementSellIn(item);
        if (item.SellIn < 0)
        {
            item.Quality = MIN_QUALITY;
        }
    }
}