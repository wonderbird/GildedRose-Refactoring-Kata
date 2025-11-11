using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Gilded Rose inventory management system orchestrator.
/// Updates item quality and sell-in values by delegating to item-specific strategies.
/// 
/// Uses Strategy pattern to handle different item types:
/// - <see cref="NormalItemStrategy"/> - Standard items with quality degradation
/// - <see cref="AgedBrieStrategy"/> - Items that improve with age
/// - <see cref="BackstagePassStrategy"/> - Items with time-sensitive value
/// - <see cref="ConjuredItemStrategy"/> - Items with accelerated degradation
/// - <see cref="SulfurasStrategy"/> - Legendary items that never change
/// 
/// For detailed business rules, see individual strategy class documentation.
/// For executable specification, see <see cref="GildedRoseTests.GildedRoseTest"/>.
/// </summary>
public class GildedRose
{
    IList<Item> Items;
    private readonly IStrategySelector _strategySelector;

    /// <summary>
    /// Initializes a new instance of the GildedRose inventory system.
    /// </summary>
    /// <param name="Items">The list of items to manage</param>
    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
        _strategySelector = new NameBasedStrategySelector();
    }

    /// <summary>
    /// Updates all items by delegating to their respective strategies.
    /// Each item's quality and sell-in values are updated according to its type-specific rules.
    /// </summary>
    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var strategy = _strategySelector.GetStrategy(item);
            strategy.UpdateItem(item);
        }
    }
}