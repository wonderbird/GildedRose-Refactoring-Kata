using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

/// <summary>
/// Tests for the Gilded Rose inventory management system.
/// 
/// Business Rules:
/// - Quality bounds: Most items have quality between 0 and 50 (inclusive).
///   Sulfuras is an exception with quality fixed at 80.
/// - Sell-by date: When SellIn reaches 0, the item is past its sell-by date.
///   Negative SellIn values indicate items well past their sell-by date.
/// 
/// Item Type Behaviors:
/// - Normal items: Quality decreases by 1 per day, by 2 after sell-by date.
/// - Aged Brie: Quality increases by 1 per day, by 2 after sell-by date.
/// - Backstage passes: Quality increases based on SellIn:
///   * SellIn > 10: +1 per day
///   * 6 <= SellIn <= 10: +2 per day
///   * 1 <= SellIn <= 5: +3 per day
///   * SellIn <= 0: Quality drops to 0
/// - Sulfuras: Legendary item that never changes (quality and SellIn remain constant).
/// </summary>
public class GildedRoseTest
{
    private const string AGED_BRIE = "Aged Brie";
    private const string BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert";
    private const string SULFURAS = "Sulfuras, Hand of Ragnaros";
    private const string NORMAL_ITEM = "Normal Item";

    private const int MAX_QUALITY = 50;
    private const int MIN_QUALITY = 0;
    private const int SULFURAS_QUALITY = 80;

    private Item CreateItemAndUpdateQuality(string name, int sellIn, int quality)
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
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 10, 20);
        
        // Assert
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void NormalItem_DecreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 10, 20);
        
        // Assert
        Assert.Equal(19, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 0, 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 5, MIN_QUALITY);
        
        // Assert
        Assert.Equal(MIN_QUALITY, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(AGED_BRIE, 10, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(AGED_BRIE, 0, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(AGED_BRIE, 10, MAX_QUALITY);
        
        // Assert
        Assert.Equal(MAX_QUALITY, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByOne_MoreThanTenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 15, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 10, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 5, 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 0, 20);
        
        // Assert
        Assert.Equal(MIN_QUALITY, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(SULFURAS, 10, SULFURAS_QUALITY);
        
        // Assert
        Assert.Equal(SULFURAS_QUALITY, item.Quality);
        Assert.Equal(10, item.SellIn);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByTwo()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 10, 49);
        
        // Assert
        Assert.Equal(MAX_QUALITY, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByThree()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 5, 48);
        
        // Assert
        Assert.Equal(MAX_QUALITY, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(AGED_BRIE, 0, 49);
        
        // Assert
        Assert.Equal(MAX_QUALITY, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityOne()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 0, 1);
        
        // Assert
        Assert.Equal(MIN_QUALITY, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_SixDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 6, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_ElevenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 11, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityZero()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, 0, MIN_QUALITY);
        
        // Assert
        Assert.Equal(MIN_QUALITY, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_OneDayBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(BACKSTAGE_PASSES, 1, 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_WellPastSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(AGED_BRIE, -5, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_WellPastSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(NORMAL_ITEM, -5, 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges_WithNegativeSellIn()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality(SULFURAS, -1, SULFURAS_QUALITY);
        
        // Assert
        Assert.Equal(SULFURAS_QUALITY, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }
}