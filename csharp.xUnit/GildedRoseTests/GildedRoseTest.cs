using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void foo()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("foo", Items[0].Name);
    }

    [Fact]
    public void RegularItem_QualityDecreasesByOne_WhenSellInIsPositive()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = 5, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(9, Items[0].Quality);
        Assert.Equal(4, Items[0].SellIn);
    }

    [Fact]
    public void RegularItem_QualityDecreasesByTwo_WhenSellInIsNegative()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Regular Item", SellIn = 0, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(8, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void AgedBrie_QualityIncreasesByOne_WhenSellInIsPositive()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(11, Items[0].Quality);
        Assert.Equal(4, Items[0].SellIn);
    }

    [Fact]
    public void AgedBrie_QualityIncreasesByTwo_WhenExpired()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(12, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void BackstagePass_QualityIncreasesByOne_WhenSellInGreaterThanTen()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(11, Items[0].Quality);
        Assert.Equal(14, Items[0].SellIn);
    }

    [Fact]
    public void BackstagePass_QualityIncreasesByTwo_WhenSellInBetweenSixAndTen()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(12, Items[0].Quality);
        Assert.Equal(9, Items[0].SellIn);
    }

    [Fact]
    public void BackstagePass_QualityIncreasesByThree_WhenSellInBetweenOneAndFive()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(13, Items[0].Quality);
        Assert.Equal(4, Items[0].SellIn);
    }

    [Fact]
    public void BackstagePass_QualityDropsToZero_WhenExpired()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(0, Items[0].Quality);
        Assert.Equal(-1, Items[0].SellIn);
    }

    [Fact]
    public void Sulfuras_NeverChanges_WhenUpdated()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };
        GildedRose app = new GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal(80, Items[0].Quality);
        Assert.Equal(0, Items[0].SellIn);
    }
}