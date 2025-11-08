using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    // Item name constants
    private const string AgedBrie = "Aged Brie";
    private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
    private const string NormalItem = "Normal Item";

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
        var item = UpdateItem(NormalItem, sellIn: 10, quality: 20);
        
        // Assert
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void NormalItem_DecreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: 10, quality: 20);
        
        // Assert
        Assert.Equal(19, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: 0, quality: 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
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
        var item = UpdateItem(AgedBrie, sellIn: 10, quality: 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_AfterSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(AgedBrie, sellIn: 0, quality: 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
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
        var item = UpdateItem(BackstagePasses, sellIn: 15, quality: 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 10, quality: 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 5, quality: 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
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
        var item = UpdateItem(BackstagePasses, sellIn: 6, quality: 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_ElevenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = UpdateItem(BackstagePasses, sellIn: 11, quality: 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
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
        var item = UpdateItem(BackstagePasses, sellIn: 1, quality: 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_WellPastSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(AgedBrie, sellIn: -5, quality: 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_WellPastSellByDate()
    {
        // Arrange & Act
        var item = UpdateItem(NormalItem, sellIn: -5, quality: 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
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
}