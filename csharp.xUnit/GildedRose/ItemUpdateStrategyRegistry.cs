using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Registry for item update strategies. Maps item names to their corresponding update strategies.
/// Provides a factory pattern for retrieving the appropriate strategy for an item.
/// </summary>
public class ItemUpdateStrategyRegistry
{
    // Item type names
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";

    private readonly Dictionary<string, IItemUpdateStrategy> _strategies;
    private readonly IItemUpdateStrategy _defaultStrategy;

    /// <summary>
    /// Initializes a new instance of the ItemUpdateStrategyRegistry with all registered strategies.
    /// </summary>
    public ItemUpdateStrategyRegistry()
    {
        _defaultStrategy = new NormalItemStrategy();
        _strategies = new Dictionary<string, IItemUpdateStrategy>
        {
            { AGED_BRIE, new AgedBrieStrategy() },
            { BACKSTAGE_PASSES, new BackstagePassStrategy() },
            { SULFURAS, new SulfurasStrategy() }
        };
    }

    /// <summary>
    /// Gets the update strategy for the specified item name.
    /// Returns the default strategy (NormalItemStrategy) if no specific strategy is found.
    /// </summary>
    /// <param name="itemName">The name of the item.</param>
    /// <returns>The update strategy for the item, or the default strategy if not found.</returns>
    public IItemUpdateStrategy GetStrategy(string itemName)
    {
        if (_strategies.TryGetValue(itemName, out var strategy))
        {
            return strategy;
        }
        return _defaultStrategy;
    }
}

