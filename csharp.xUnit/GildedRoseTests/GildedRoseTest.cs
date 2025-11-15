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
}