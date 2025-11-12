using System;

namespace GildedRoseKata;

/// <summary>
/// Base class for item update strategies providing common helper methods.
/// Reduces duplication across strategy implementations.
/// </summary>
public abstract class BaseItemUpdateStrategy : IItemUpdateStrategy
{
    // Quality bounds
    protected const int MAX_QUALITY = 50;
    protected const int MIN_QUALITY = 0;

    /// <summary>
    /// Updates the quality and sell-in values of an item according to the strategy's rules.
    /// Must be implemented by concrete strategy classes.
    /// </summary>
    /// <param name="item">The item to update.</param>
    public abstract void Update(Item item);

    /// <summary>
    /// Decreases the quality of an item by the specified amount, ensuring it does not go below the minimum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be decreased.</param>
    /// <param name="amount">The amount by which to decrease the quality.</param>
    protected void DecreaseQuality(Item item, int amount)
    {
        item.Quality = Math.Max(MIN_QUALITY, item.Quality - amount);
    }

    /// <summary>
    /// Increases the quality of an item by the specified amount, ensuring it does not exceed the maximum quality.
    /// </summary>
    /// <param name="item">The item whose quality should be increased.</param>
    /// <param name="amount">The amount by which to increase the quality.</param>
    protected void IncreaseQuality(Item item, int amount)
    {
        item.Quality = Math.Min(MAX_QUALITY, item.Quality + amount);
    }

    /// <summary>
    /// Decrements the sell-in value of an item by 1.
    /// </summary>
    /// <param name="item">The item whose sell-in value should be decremented.</param>
    protected void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }

    /// <summary>
    /// Checks if an item is past its sell-by date.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>True if the item's SellIn is less than 0, false otherwise.</returns>
    protected bool IsPastSellByDate(Item item)
    {
        return item.SellIn < 0;
    }
}

