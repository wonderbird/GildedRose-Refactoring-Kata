namespace GildedRoseKata;

/// <summary>
/// Strategy for updating Sulfuras: legendary item that never changes (no-op).
/// </summary>
public class SulfurasStrategy : BaseItemUpdateStrategy
{
    /// <summary>
    /// Updates Sulfuras: legendary item that never changes (no-op).
    /// </summary>
    /// <param name="item">The Sulfuras item to update.</param>
    public override void Update(Item item)
    {
        // Sulfuras never changes - no operation needed
    }
}

