using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

/// <summary>
/// Tests for the Gilded Rose inventory management system.
/// 
/// BUSINESS RULES OVERVIEW:
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
public class GildedRoseTest
{
    // Item name constants
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string NormalItem = "Normal Item";
    private const string ConjuredItem = "Conjured Mana Cake";

    // Quality boundary constants
    private const int MinQuality = 0;
    private const int MaxQuality = 50;
    private const int SulfurasQuality = 80;

    /// <summary>
    /// Helper method to create an item, run UpdateQuality once, and return the updated item.
    /// </summary>
    private Item UpdateItem(string name, int sellIn, int quality)
    {
        var items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        return items[0];
    }

    [Fact]
    public void NormalItem_DecreaseSellIn_AfterOneDay()
    {
        // Arrange & Act
        const int initialSellIn = 10;
        var item = UpdateItem(NormalItem, sellIn: initialSellIn, quality: 20);
        
        // Assert
        Assert.Equal(initialSellIn - 1, item.SellIn);
    }

    [Fact]
    public void NormalItem_DecreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(NormalItem, sellIn: 10, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 1, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(NormalItem, sellIn: 0, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 2, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityByOne_OnSellByDateBoundary()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(NormalItem, sellIn: 1, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 1, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: 5, quality: MinQuality);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(AgedBrie, sellIn: 10, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 1, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_AfterSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(AgedBrie, sellIn: 0, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 2, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityByOne_OnSellByDateBoundary()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(AgedBrie, sellIn: 1, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 1, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty()
    {
        // Arrange & Act
        var item = UpdateItem(AgedBrie, sellIn: 10, quality: MaxQuality);
        
        // Assert
        Assert.Equal(MaxQuality, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByOne_MoreThanTenDaysBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 15, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 1, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 10, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 2, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 5, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 3, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 0, quality: 20);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges()
    {
        // Arrange & Act
        var item = UpdateItem(Sulfuras, sellIn: 10, quality: SulfurasQuality);
        
        // Assert
        Assert.Equal(SulfurasQuality, item.Quality);
        Assert.Equal(10, item.SellIn);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByTwo()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 10, quality: 49);
        
        // Assert
        Assert.Equal(MaxQuality, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByThree()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 5, quality: 48);
        
        // Assert
        Assert.Equal(MaxQuality, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty_AfterSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(AgedBrie, sellIn: 0, quality: 49);
        
        // Assert
        Assert.Equal(MaxQuality, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityOne()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: 0, quality: 1);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_SixDaysBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 6, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 2, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_ElevenDaysBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 11, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 1, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityZero()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: 0, quality: MinQuality);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_OneDayBeforeConcert()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(BackstagePasses, sellIn: 1, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 3, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_WellPastSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(AgedBrie, sellIn: -5, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality + 2, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_WellPastSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(NormalItem, sellIn: -5, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 2, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges_WithNegativeSellIn()
    {
        // Arrange & Act
        var item = UpdateItem(Sulfuras, sellIn: -1, quality: SulfurasQuality);
        
        // Assert
        Assert.Equal(SulfurasQuality, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void ConjuredItem_DecreaseQualityByTwo_BeforeSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(ConjuredItem, sellIn: 10, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 2, item.Quality);
    }

    [Fact]
    public void ConjuredItem_DecreaseQualityByFour_AfterSellByDate()
    {
        // Arrange & Act
        const int initialQuality = 20;
        var item = UpdateItem(ConjuredItem, sellIn: 0, quality: initialQuality);
        
        // Assert
        Assert.Equal(initialQuality - 4, item.Quality);
    }

    [Fact]
    public void ConjuredItem_QualityNeverNegative()
    {
        // Arrange & Act
        var item = UpdateItem(ConjuredItem, sellIn: 5, quality: MinQuality);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void ConjuredItem_QualityNeverNegative_AfterSellByDateWithQualityOne()
    {
        // Arrange & Act
        var item = UpdateItem(ConjuredItem, sellIn: 0, quality: 1);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }

    [Fact]
    public void ConjuredItem_QualityNeverNegative_AfterSellByDateWithQualityThree()
    {
        // Arrange & Act
        var item = UpdateItem(ConjuredItem, sellIn: 0, quality: 3);
        
        // Assert
        Assert.Equal(MinQuality, item.Quality);
    }
}