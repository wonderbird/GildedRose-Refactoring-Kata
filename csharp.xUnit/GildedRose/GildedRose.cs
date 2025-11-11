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
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string Conjured = "Conjured Mana Cake";

    private const int MaxQuality = 50;
    private const int MinQuality = 0;
    private const int BackstageFirstTierBoundary = 11;
    private const int BackstageSecondTierBoundary = 6;

    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    private bool IsAgedBrie(Item item) => item.Name == AgedBrie;
    
    private bool IsBackstagePass(Item item) => item.Name == BackstagePasses;
    
    private bool IsSulfuras(Item item) => item.Name == Sulfuras;
    
    private bool IsConjured(Item item) => item.Name == Conjured;
    
    private bool IsAtMaxQuality(Item item) => item.Quality >= MaxQuality;
    
    private bool IsAtMinQuality(Item item) => item.Quality <= MinQuality;

    private void DecreaseQuality(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality--;
        }
    }

    private void IncreaseQuality(Item item)
    {
        if (!IsAtMaxQuality(item))
        {
            item.Quality++;
        }
    }

    private void DecrementSellIn(Item item)
    {
        item.SellIn--;
    }

    private void UpdateNormalItem(Item item)
    {
        DecreaseQuality(item);

        DecrementSellIn(item);

        if (item.SellIn < 0)
        {
            DecreaseQuality(item);
        }
    }

    private void UpdateAgedBrie(Item item)
    {
        IncreaseQuality(item);

        DecrementSellIn(item);

        if (item.SellIn < 0)
        {
            IncreaseQuality(item);
        }
    }

    private void UpdateBackstagePass(Item item)
    {
        IncreaseQuality(item);

        if (item.SellIn < BackstageFirstTierBoundary)
        {
            IncreaseQuality(item);
        }

        if (item.SellIn < BackstageSecondTierBoundary)
        {
            IncreaseQuality(item);
        }

        DecrementSellIn(item);

        if (item.SellIn < 0)
        {
            item.Quality = MinQuality;
        }
    }

    private void UpdateConjuredItem(Item item)
    {
        if (!IsAtMinQuality(item))
        {
            item.Quality = System.Math.Max(MinQuality, item.Quality - 2);
        }

        DecrementSellIn(item);

        if (item.SellIn < 0)
        {
            if (!IsAtMinQuality(item))
            {
                item.Quality = System.Math.Max(MinQuality, item.Quality - 2);
            }
        }
    }

    public void UpdateQuality()
    {
        foreach (var item in Items)
        {
            // Sulfuras never changes
            if (IsSulfuras(item))
            {
                continue;
            }

            if (IsAgedBrie(item))
            {
                UpdateAgedBrie(item);
            }
            else if (IsBackstagePass(item))
            {
                UpdateBackstagePass(item);
            }
            else if (IsConjured(item))
            {
                UpdateConjuredItem(item);
            }
            else
            {
                UpdateNormalItem(item);
            }
        }
    }
}