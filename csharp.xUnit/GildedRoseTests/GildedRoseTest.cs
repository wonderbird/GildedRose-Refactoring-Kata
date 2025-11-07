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
}