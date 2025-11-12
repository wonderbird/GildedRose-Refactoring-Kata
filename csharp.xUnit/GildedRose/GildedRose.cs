using System;
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
    private readonly Dictionary<string, Action<Item>> _updateStrategies;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
        _updateStrategies = new Dictionary<string, Action<Item>>
        {
            { AGED_BRIE, UpdateAgedBrie },
            { BACKSTAGE_PASSES, UpdateBackstagePass }
        };
    }

    /// <summary>
    /// Updates the quality and sell-in values for all items in the inventory.
    /// Each item type follows specific rules for quality degradation or improvement.
    /// </summary>
    public void UpdateQuality()
    {
        for (var i = 0; i < Items.Count; i++)
        {
            if (IsSulfuras(Items[i]))
            {
                continue;
            }
            
            if (_updateStrategies.TryGetValue(Items[i].Name, out var updateStrategy))
            {
                updateStrategy(Items[i]);
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
            item.Quality = Math.Max(MIN_QUALITY, item.Quality - amount);
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
            item.Quality = Math.Min(MAX_QUALITY, item.Quality + amount);
        }
    }

    /// <summary>
    /// Decrements the sell-in value of an item by 1.
    /// </summary>
    /// <param name="item">The item whose sell-in value should be decremented.</param>
    private void DecrementSellIn(Item item)
    {
        item.SellIn = item.SellIn - 1;
    }

    private bool IsPastSellByDate(Item item)
    {
        return item.SellIn < 0;
    }

    private bool IsSulfuras(Item item)
    {
        return item.Name == SULFURAS;
    }


    /// <summary>
    /// Updates an item with a common pattern: updates quality, decrements sell-in, and updates quality again if past sell-by date.
    /// </summary>
    /// <param name="item">The item to update.</param>
    /// <param name="updateQuality">The quality update function to use.</param>
    private void UpdateItem(Item item, Action<Item, int> updateQuality)
    {
        updateQuality(item, 1);
        DecrementSellIn(item);
        if (IsPastSellByDate(item))
        {
            updateQuality(item, 1);
        }
    }

    /// <summary>
    /// Updates a normal item: decreases quality by 1, decrements sell-in, and decreases quality by 1 again if past sell-by date.
    /// </summary>
    /// <param name="item">The normal item to update.</param>
    private void UpdateNormalItem(Item item)
    {
        UpdateItem(item, DecreaseQuality);
    }

    /// <summary>
    /// Updates Aged Brie: increases quality by 1, decrements sell-in, and increases quality by 1 again if past sell-by date.
    /// </summary>
    /// <param name="item">The Aged Brie item to update.</param>
    private void UpdateAgedBrie(Item item)
    {
        UpdateItem(item, IncreaseQuality);
    }

    /// <summary>
    /// Updates Backstage passes: increases quality based on sell-in thresholds, decrements sell-in, and sets quality to 0 if past concert date.
    /// </summary>
    /// <param name="item">The Backstage pass item to update.</param>
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
        if (IsPastSellByDate(item))
        {
            item.Quality = MIN_QUALITY;
        }
    }
}