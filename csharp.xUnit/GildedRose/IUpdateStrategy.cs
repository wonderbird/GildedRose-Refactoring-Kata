namespace GildedRoseKata;

/// <summary>
/// Strategy interface for updating item quality and sell-in values.
/// Each item type has its own implementation with specific business rules.
/// </summary>
public interface IUpdateStrategy
{
    /// <summary>
    /// Updates the item's quality and sell-in values according to the strategy's rules.
    /// </summary>
    /// <param name="item">The item to update</param>
    void UpdateItem(Item item);
}

