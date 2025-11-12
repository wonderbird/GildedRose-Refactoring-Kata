using System;
using System.Collections.Generic;

namespace GildedRoseKata;

public class GildedRose
{
    // Item type names
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    
    // Quality bounds
    private const int MAX_QUALITY = 50;
    private const int MIN_QUALITY = 0;
    
    // Backstage pass thresholds
    private const int BACKSTAGE_PASS_FIRST_THRESHOLD = 11;
    private const int BACKSTAGE_PASS_SECOND_THRESHOLD = 6;
    IList<Item> Items;
    private readonly ItemUpdateStrategyRegistry _strategyRegistry;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
        _strategyRegistry = new ItemUpdateStrategyRegistry();
    }

    /// <summary>
    /// Updates the quality and sell-in values for all items in the inventory.
    /// Each item type follows specific rules for quality degradation or improvement.
    /// </summary>
    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var strategy = _strategyRegistry.GetStrategy(item.Name);
            strategy.Update(item);
        }
    }

    /// <summary>
    /// Decreases the quality of an item by the specified amount, ensuring it does not go below the minimum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be decreased.</param>
    /// <param name="amount">The amount by which to decrease the quality.</param>
    private void DecreaseQuality(Item item, int amount)
    {
        item.Quality = Math.Max(MIN_QUALITY, item.Quality - amount);
    }

    /// <summary>
    /// Increases the quality of an item by the specified amount, ensuring it does not exceed the maximum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be increased.</param>
    /// <param name="amount">The amount by which to increase the quality.</param>
    private void IncreaseQuality(Item item, int amount)
    {
        item.Quality = Math.Min(MAX_QUALITY, item.Quality + amount);
    }

    /// <summary>
    /// Decrements the sell-in value of an item by 1.
    /// </summary>
    /// <param name="item">The item whose sell-in value should be decremented.</param>
    private void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }

    private bool IsPastSellByDate(Item item)
    {
        return item.SellIn < 0;
    }

    /// <summary>
    /// Updates Sulfuras: legendary item that never changes (no-op).
    /// </summary>
    /// <param name="item">The Sulfuras item to update.</param>
    private void UpdateSulfuras(Item item)
    {
        // Sulfuras never changes - no operation needed
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
    /// Calculates the quality increment amount for Backstage passes based on sell-in thresholds.
    /// </summary>
    /// <param name="sellIn">The sell-in value of the item.</param>
    /// <returns>The amount by which to increase quality (1, 2, or 3).</returns>
    private int CalculateBackstagePassIncrement(int sellIn)
    {
        int increment = 1;
        if (sellIn < BACKSTAGE_PASS_FIRST_THRESHOLD)
        {
            increment++;
        }
        if (sellIn < BACKSTAGE_PASS_SECOND_THRESHOLD)
        {
            increment++;
        }
        return increment;
    }

    /// <summary>
    /// Updates Backstage passes: increases quality based on sell-in thresholds, decrements sell-in, and sets quality to 0 if past concert date.
    /// </summary>
    /// <param name="item">The Backstage pass item to update.</param>
    private void UpdateBackstagePass(Item item)
    {
        int increment = CalculateBackstagePassIncrement(item.SellIn);
        IncreaseQuality(item, increment);
        DecrementSellIn(item);
        if (IsPastSellByDate(item))
        {
            item.Quality = MIN_QUALITY;
        }
    }
}