namespace GildedRoseKata;

/// <summary>
/// Base class for update strategies providing shared constants and helper methods.
/// </summary>
public abstract class BaseUpdateStrategy : IUpdateStrategy
{
    protected const int MaxQuality = 50;
    protected const int MinQuality = 0;

    public abstract void UpdateItem(Item item);
}

