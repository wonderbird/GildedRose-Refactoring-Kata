using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void NormalItem_DecreaseSellIn_AfterOneDay()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 10, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(9, items[0].SellIn);
    }

    [Fact]
    public void NormalItem_DecreaseQuality_BeforeSellByDate()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 10, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(19, items[0].Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 0, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(18, items[0].Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 5, Quality = 0 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQuality_BeforeSellByDate()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(21, items[0].Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_AfterSellByDate()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(22, items[0].Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByOne_MoreThanTenDaysBeforeConcert()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(21, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(22, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(23, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(0, items[0].Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(80, items[0].Quality);
        Assert.Equal(10, items[0].SellIn);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByTwo()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 49 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByThree()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 48 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty_AfterSellByDate()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(50, items[0].Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityOne()
    {
        // Arrange
        var items = new List<Item> { new Item { Name = "Normal Item", SellIn = 0, Quality = 1 } };
        var app = new GildedRose(items);
        
        // Act
        app.UpdateQuality();
        
        // Assert
        Assert.Equal(0, items[0].Quality);
    }
}