using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Manages a collection of items and updates their quality and sell-in values
/// using the Strategy Pattern. Delegates item-specific update logic to strategy classes.
/// </summary>
public class GildedRose
{
    IList<Item> Items;
    private readonly ItemUpdateStrategyRegistry _strategyRegistry;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
        _strategyRegistry = new ItemUpdateStrategyRegistry();
    }

    /// <summary>
    /// Updates the quality and sell-in values for all items in the inventory.
    /// Each item type follows specific rules for quality degradation or improvement,
    /// implemented by their respective strategy classes.
    /// </summary>
    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var strategy = _strategyRegistry.GetStrategy(item.Name);
            strategy.Update(item);
        }
    }
}