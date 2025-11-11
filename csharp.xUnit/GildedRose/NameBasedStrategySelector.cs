using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Selects update strategies based on item name.
/// </summary>
public class NameBasedStrategySelector : IStrategySelector
{
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Conjured = "Conjured Mana Cake";

    private readonly Dictionary<string, IUpdateStrategy> _strategies;
    private readonly IUpdateStrategy _defaultStrategy;

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

    public IUpdateStrategy GetStrategy(Item item)
    {
        return _strategies.TryGetValue(item.Name, out var strategy)
            ? strategy
            : _defaultStrategy;
    }
}

