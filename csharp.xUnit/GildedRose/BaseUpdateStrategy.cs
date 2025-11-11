namespace GildedRoseKata;

/// <summary>
/// Base class for update strategies using Template Method pattern.
/// Provides shared constants and helper methods to eliminate duplication across strategies.
/// Derived classes override <see cref="UpdateItem"/> to implement item-specific business rules.
/// </summary>
public abstract class BaseUpdateStrategy : IUpdateStrategy
{
    /// <summary>Maximum quality value for normal items (Sulfuras is an exception with quality 80).</summary>
    protected const int MaxQuality = 50;
    
    /// <summary>Minimum quality value for all items.</summary>
    protected const int MinQuality = 0;

    /// <summary>
    /// Updates the item's quality and sell-in values according to item-specific rules.
    /// </summary>
    /// <param name="item">The item to update</param>
    public abstract void UpdateItem(Item item);

    /// <summary>Determines if an item is past its sell-by date (SellIn &lt; 0).</summary>
    protected bool IsPastSellByDate(Item item) => item.SellIn < 0;

    /// <summary>Determines if an item has reached maximum quality.</summary>
    protected bool IsAtMaxQuality(Item item) => item.Quality >= MaxQuality;

    /// <summary>Determines if an item has reached minimum quality.</summary>
    protected bool IsAtMinQuality(Item item) => item.Quality <= MinQuality;

    /// <summary>Decrements the item's sell-in value by 1.</summary>
    protected void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }

    /// <summary>Increases the item's quality by 1, respecting maximum quality boundary.</summary>
    protected void IncreaseQuality(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality++;
        }
    }

    /// <summary>Decreases the item's quality by 1, respecting minimum quality boundary.</summary>
    protected void DecreaseQuality(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality--;
        }
    }
}

