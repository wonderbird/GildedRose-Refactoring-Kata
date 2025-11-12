namespace GildedRoseKata;

/// <summary>
/// Strategy interface for updating item quality and sell-in values.
/// Each item type implements this interface to define its specific update behavior.
/// </summary>
public interface IItemUpdateStrategy
{
    /// <summary>
    /// Updates the quality and sell-in values of an item according to the strategy's rules.
    /// </summary>
    /// <param name="item">The item to update.</param>
    void Update(Item item);
}

