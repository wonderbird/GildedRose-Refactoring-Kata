using System.Collections.Generic;

namespace GildedRoseKata;

/// <summary>
/// Gilded Rose inventory management system.
/// Updates the quality and sell-by dates of items according to specific business rules.
/// 
/// BUSINESS RULES:
/// 
/// Items have three properties:
/// - Name: Identifies the item type
/// - SellIn: Days remaining until the sell-by date (decrements daily, except Sulfuras)
/// - Quality: Value/quality of the item
/// 
/// QUALITY BOUNDS:
/// - Normal items: Quality ranges from 0 to 50
/// - Sulfuras (legendary): Quality is always 80 (never changes)
/// 
/// SELL-BY DATE CONCEPT:
/// - When SellIn reaches 0 or below, the item is past its sell-by date
/// - Quality degradation typically accelerates after the sell-by date
/// 
/// ITEM TYPE BEHAVIORS:
/// 
/// 1. Normal Items:
///    - Quality decreases by 1 per day before sell-by date
///    - Quality decreases by 2 per day after sell-by date
///    - Quality never goes below 0
/// 
/// 2. Aged Brie:
///    - Quality increases by 1 per day before sell-by date
///    - Quality increases by 2 per day after sell-by date
///    - Quality never exceeds 50
/// 
/// 3. Backstage Passes (to a TAFKAL80ETC concert):
///    - Quality increases as concert approaches:
///      * +1 per day when SellIn > 10
///      * +2 per day when SellIn is 6-10
///      * +3 per day when SellIn is 1-5
///    - Quality drops to 0 immediately after concert (SellIn ≤ 0)
///    - Quality never exceeds 50 before concert
/// 
/// 4. Sulfuras (legendary item):
///    - Quality never changes (always 80)
///    - SellIn never changes
///    - Immune to all degradation rules
/// 
/// 5. Conjured Items:
///    - Quality decreases by 2 per day before sell-by date (twice as fast as normal)
///    - Quality decreases by 4 per day after sell-by date (twice as fast as normal)
///    - Quality never goes below 0
/// </summary>
public class GildedRose
{
    IList<Item> Items;
    private readonly IStrategySelector _strategySelector;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
        _strategySelector = new NameBasedStrategySelector();
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            var strategy = _strategySelector.GetStrategy(item);
            strategy.UpdateItem(item);
        }
    }
}