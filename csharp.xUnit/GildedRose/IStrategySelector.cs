namespace GildedRoseKata;

/// <summary>
/// Interface for selecting the appropriate update strategy for an item.
/// </summary>
public interface IStrategySelector
{
    /// <summary>
    /// Gets the appropriate update strategy for the given item.
    /// </summary>
    /// <param name="item">The item to get a strategy for</param>
    /// <returns>The update strategy for the item</returns>
    IUpdateStrategy GetStrategy(Item item);
}

