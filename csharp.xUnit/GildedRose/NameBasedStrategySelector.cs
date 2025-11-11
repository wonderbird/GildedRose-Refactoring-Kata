using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Selects update strategies based on item name using Factory pattern.
/// Maps known item names to specific strategies and provides a default strategy for unknown items.
/// </summary>
public class NameBasedStrategySelector : IStrategySelector
{
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Conjured = "Conjured Mana Cake";

    private readonly Dictionary<string, IUpdateStrategy> _strategies;
    private readonly IUpdateStrategy _defaultStrategy;

    /// <summary>
    /// Initializes the selector with strategies for all known item types.
    /// </summary>
    public NameBasedStrategySelector()
    {
        _strategies = new Dictionary<string, IUpdateStrategy>
        {
            { AgedBrie, new AgedBrieStrategy() },
            { BackstagePasses, new BackstagePassStrategy() },
            { Sulfuras, new SulfurasStrategy() },
            { Conjured, new ConjuredItemStrategy() }
        };
        _defaultStrategy = new NormalItemStrategy();
    }

    /// <summary>
    /// Gets the appropriate update strategy for the given item based on its name.
    /// </summary>
    /// <param name="item">The item to get a strategy for</param>
    /// <returns>The update strategy for the item, or the default strategy if item name is not recognized</returns>
    public IUpdateStrategy GetStrategy(Item item)
    {
        return _strategies.TryGetValue(item.Name, out var strategy)
            ? strategy
            : _defaultStrategy;
    }
}

