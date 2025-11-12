namespace GildedRoseKata;

/// <summary>
/// Strategy for updating normal items: decreases quality by 1, decrements sell-in,
/// and decreases quality by 1 again if past sell-by date.
/// </summary>
public class NormalItemStrategy : BaseItemUpdateStrategy
{
    /// <summary>
    /// Updates a normal item: decreases quality by 1, decrements sell-in,
    /// and decreases quality by 1 again if past sell-by date.
    /// </summary>
    /// <param name="item">The normal item to update.</param>
    public override void Update(Item item)
    {
        DecreaseQuality(item, 1);
        DecrementSellIn(item);
        if (IsPastSellByDate(item))
        {
            DecreaseQuality(item, 1);
        }
    }
}

