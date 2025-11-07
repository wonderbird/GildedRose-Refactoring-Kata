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
}