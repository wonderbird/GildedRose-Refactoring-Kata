using Xunit;
using GildedRoseKata;

namespace GildedRoseTests;

public class ItemUpdaterTest
{
    [Fact]
    public void IItemUpdater_CanBeDeclared()
    {
        IItemUpdater updater = null;
        Assert.Null(updater);
    }

    [Fact]
    public void RegularItemUpdater_DecreasesQualityByOne_WhenSellInIsPositive()
    {
        var item = new Item { Name = "Regular Item", SellIn = 5, Quality = 10 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(9, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void RegularItemUpdater_DecreasesQualityByTwo_WhenExpired()
    {
        var item = new Item { Name = "Regular Item", SellIn = 0, Quality = 10 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(8, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void RegularItemUpdater_DoesNotDecreaseQuality_WhenQualityIsZero()
    {
        var item = new Item { Name = "Regular Item", SellIn = 5, Quality = 0 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(0, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void RegularItemUpdater_DoesNotDecreaseQualityBelowZero_WhenExpired()
    {
        var item = new Item { Name = "Regular Item", SellIn = 0, Quality = 1 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(0, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void RegularItemUpdater_DoesNotDecreaseQuality_WhenQualityIsZeroAndExpired()
    {
        var item = new Item { Name = "Regular Item", SellIn = 0, Quality = 0 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(0, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void RegularItemUpdater_DoesNotApplyExpiredLogic_WhenSellInBecomesZero()
    {
        var item = new Item { Name = "Regular Item", SellIn = 1, Quality = 10 };
        IItemUpdater updater = new RegularItemUpdater();
        updater.Update(item);
        Assert.Equal(9, item.Quality);
        Assert.Equal(0, item.SellIn);
    }

    [Fact]
    public void AgedBrieUpdater_IncreasesQualityByOne_WhenSellInIsPositive()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 10 };
        IItemUpdater updater = new AgedBrieUpdater();
        updater.Update(item);
        Assert.Equal(11, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void AgedBrieUpdater_IncreasesQualityByTwo_WhenExpired()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 10 };
        IItemUpdater updater = new AgedBrieUpdater();
        updater.Update(item);
        Assert.Equal(12, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void AgedBrieUpdater_DoesNotIncreaseQuality_WhenQualityIsFifty()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 };
        IItemUpdater updater = new AgedBrieUpdater();
        updater.Update(item);
        Assert.Equal(50, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void AgedBrieUpdater_DoesNotIncreaseQualityAboveFifty_WhenExpired()
    {
        var item = new Item { Name = "Aged Brie", SellIn = 0, Quality = 49 };
        IItemUpdater updater = new AgedBrieUpdater();
        updater.Update(item);
        Assert.Equal(50, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }

    [Fact]
    public void BackstagePassUpdater_IncreasesQualityByOne_WhenSellInGreaterThanTen()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 10 };
        IItemUpdater updater = new BackstagePassUpdater();
        updater.Update(item);
        Assert.Equal(11, item.Quality);
        Assert.Equal(14, item.SellIn);
    }

    [Fact]
    public void BackstagePassUpdater_IncreasesQualityByTwo_WhenSellInBetweenSixAndTen()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 };
        IItemUpdater updater = new BackstagePassUpdater();
        updater.Update(item);
        Assert.Equal(12, item.Quality);
        Assert.Equal(9, item.SellIn);
    }

    [Fact]
    public void BackstagePassUpdater_IncreasesQualityByThree_WhenSellInBetweenOneAndFive()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10 };
        IItemUpdater updater = new BackstagePassUpdater();
        updater.Update(item);
        Assert.Equal(13, item.Quality);
        Assert.Equal(4, item.SellIn);
    }

    [Fact]
    public void BackstagePassUpdater_DropsQualityToZero_WhenExpired()
    {
        var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 };
        IItemUpdater updater = new BackstagePassUpdater();
        updater.Update(item);
        Assert.Equal(0, item.Quality);
        Assert.Equal(-1, item.SellIn);
    }
}

