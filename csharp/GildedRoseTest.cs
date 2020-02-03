using System.Collections.Generic;
using NUnit.Framework;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item> {new Item {Name = "foo", SellIn = 0, Quality = 0}};
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("fixme", Items[0].Name);
        }

        [Test]
        public void UpdateQuality_SellInLarger0_ReducesQualityAndSellIn()
        {
            var arbitraryName = "Something";

            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality - 1;

            var item = new Item
            {
                Name = arbitraryName,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> {item};

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_SellIn0_ReducesQualityBy2AndSellIn()
        {
            var arbitraryName = "Something";

            var actualSellIn = 0;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality - 2;

            var item = new Item
            {
                Name = arbitraryName,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_Quality0_DoesNotReduceQuality()
        {
            var arbitraryName = "Something";

            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 0;
            var expectedQuality = actualQuality;

            var item = new Item
            {
                Name = arbitraryName,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }       
        
        [Test]
        public void UpdateQuality_AgedBrie_IncreasesInQuality()
        {
            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality + 1;

            var item = new Item
            {
                Name = ProductNames.AgedBrie,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_AgedBrieQuality50_QualityStays50()
        {
            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 50;
            var expectedQuality = actualQuality;

            var item = new Item
            {
                Name = ProductNames.AgedBrie,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_AgedBrieWithSellIn0_QualityIncreasesBy2()
        {
            var actualSellIn = 0;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality + 2;

            var item = new Item
            {
                Name = ProductNames.AgedBrie,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_Sulfuras_DoesNotDecreaseQuality()
        {
            var actualSellIn = 5;
            var expectedSellIn = actualSellIn;

            var actualQuality = 30;
            var expectedQuality = actualQuality;

            var item = new Item
            {
                Name = ProductNames.Sulfuras,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [TestCase(11, 10, 20, 21)]
        [TestCase(10, 9, 20, 22)]
        [TestCase(6, 5, 20, 22)]
        [TestCase(5, 4, 20, 23)]
        [TestCase(1, 0, 20, 23)]
        [TestCase(0, -1, 20, 0)]
        public void UpdateQuality_BackstagePasses_QualityAsExpected(int actualSellIn, int expectedSellIn, int actualQuality, int expectedQuality)
        {
            var item = new Item
            {
                Name = ProductNames.BackstagePasses,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Test]
        public void UpdateQuality_ItemIsConjured_DegradesQualityBy2()
        {
            var conjuredItemName = ProductNames.Conjured;

            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality - 2 * GildedRose.QualityDecreaseStep;

            var item = new Item
            {
                Name = conjuredItemName,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }       
        
        [Test]
        public void UpdateQuality_ItemIsSummoned_DegradesQualityBy3()
        {
            var summonedItemName = ProductAttributes.Summoned + " Something";

            var actualSellIn = 5;
            var expectedSellIn = actualSellIn - 1;

            var actualQuality = 10;
            var expectedQuality = actualQuality - 3 * GildedRose.QualityDecreaseStep;

            var item = new Item
            {
                Name = summonedItemName,
                Quality = actualQuality,
                SellIn = actualSellIn
            };
            var items = new List<Item> { item };

            var rose = new GildedRose(items);
            rose.UpdateQuality();

            Assert.AreEqual(expectedSellIn, item.SellIn);
            Assert.AreEqual(expectedQuality, item.Quality);
        }
    }
}