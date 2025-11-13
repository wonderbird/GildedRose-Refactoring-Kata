using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
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
        var item = CreateItemAndUpdateQuality("Normal Item", 10, 20);
        
        // Assert
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void NormalItem_DecreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", 10, 20);
        
        // Assert
        Assert.Equal(19, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", 0, 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", 5, 0);
        
        // Assert
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQuality_BeforeSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Aged Brie", 10, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Aged Brie", 0, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Aged Brie", 10, 50);
        
        // Assert
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByOne_MoreThanTenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 15, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 10, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 5, 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityDropsToZero_AfterConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 0, 20);
        
        // Assert
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Sulfuras, Hand of Ragnaros", 10, 80);
        
        // Assert
        Assert.Equal(80, item.Quality);
        Assert.Equal(10, item.SellIn);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByTwo()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 10, 49);
        
        // Assert
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void BackstagePasses_QualityNeverExceedsFifty_WhenIncreasingByThree()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 5, 48);
        
        // Assert
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void AgedBrie_QualityNeverExceedsFifty_AfterSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Aged Brie", 0, 49);
        
        // Assert
        Assert.Equal(50, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityOne()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", 0, 1);
        
        // Assert
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_SixDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 6, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByTwo_ElevenDaysBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 11, 20);
        
        // Assert
        Assert.Equal(21, item.Quality);
    }

    [Fact]
    public void NormalItem_QualityNeverNegative_AfterSellByDateWithQualityZero()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", 0, 0);
        
        // Assert
        Assert.Equal(0, item.Quality);
    }

    [Fact]
    public void BackstagePasses_IncreaseQualityByThree_OneDayBeforeConcert()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Backstage passes to a TAFKAL80ETC concert", 1, 20);
        
        // Assert
        Assert.Equal(23, item.Quality);
    }

    [Fact]
    public void AgedBrie_IncreaseQualityFaster_WellPastSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Aged Brie", -5, 20);
        
        // Assert
        Assert.Equal(22, item.Quality);
    }

    [Fact]
    public void NormalItem_DecreaseQualityTwiceAsFast_WellPastSellByDate()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Normal Item", -5, 20);
        
        // Assert
        Assert.Equal(18, item.Quality);
    }

    [Fact]
    public void Sulfuras_NeverChanges_WithNegativeSellIn()
    {
        // Arrange & Act
        var item = CreateItemAndUpdateQuality("Sulfuras, Hand of Ragnaros", -1, 80);
        
        // Assert
        Assert.Equal(80, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }
}