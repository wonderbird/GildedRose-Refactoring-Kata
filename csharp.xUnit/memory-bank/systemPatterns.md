# System Patterns

## Current Architecture
The system consists of:
- `Item` class: Data class with Name, SellIn, Quality properties
- `GildedRose` class: Contains business logic in `UpdateQuality()` method
- `Program` class: Entry point that demonstrates the system

## Current Design Issues
- **Deeply nested conditionals**: The `UpdateQuality()` method has 4-5 levels of nested if statements
- **String-based type checking**: Item types identified by name string comparisons
- **Single Responsibility Violation**: `UpdateQuality()` handles all item type logic
- **Open/Closed Principle Violation**: Adding new item types requires modifying existing code

## Identified Item Types
1. **Regular Items**: Default behavior (quality decreases)
2. **Aged Brie**: Quality increases over time
3. **Backstage Passes**: Quality increases based on sell date, drops to 0 after
4. **Sulfuras**: Legendary item, never changes
5. **Conjured Items**: Quality decreases twice as fast (not fully implemented)

## Proposed Design Pattern
**Strategy Pattern**: Each item type will have its own update strategy class implementing a common interface.

### Structure
```
IItemUpdater (interface)
├── RegularItemUpdater
├── AgedBrieUpdater
├── BackstagePassUpdater
├── SulfurasUpdater
└── ConjuredItemUpdater
```

### Benefits
- Eliminates nested conditionals
- Makes adding new item types easy (Open/Closed Principle)
- Each strategy class has single responsibility
- Easier to test individual strategies
- More readable and maintainable

## Component Relationships
- `GildedRose` will use a factory or dictionary to select appropriate updater
- Updaters will operate on `Item` objects (read-only access pattern)
- Tests will verify each strategy independently

