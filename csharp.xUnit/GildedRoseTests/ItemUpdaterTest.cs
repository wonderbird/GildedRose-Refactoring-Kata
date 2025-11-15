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
}

