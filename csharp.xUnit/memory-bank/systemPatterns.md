# System Patterns

## Architecture
Simple object-oriented design with two main classes:
- **Item**: Data class holding item properties (Name, SellIn, Quality)
- **GildedRose**: Business logic class with UpdateQuality method

## Key Technical Decisions
- GildedRose operates on a list of items passed to constructor
- UpdateQuality mutates the items in place
- No return value from UpdateQuality (void method)
- Item properties are mutable (get/set)

## Design Patterns in Use

### Current Implementation (Mass: ~177)

Procedural conditional logic with high complexity:
- **16 conditionals** (64 mass) - nested if-else statements
- **8 assignments** (48 mass) - in-place quality/SellIn mutations
- **1 loop** (5 mass) - iterate through items
- **~20 invocations** (40 mass) - property accesses, comparisons
- **~15 constants** (15 mass) - string literals, numbers
- **~5 bindings** (5 mass) - variables and parameters

Key complexity drivers:
- Item type discrimination via string comparison
- Deeply nested conditionals (up to 5 levels)
- Duplicated quality boundary checks
- Repeated Sulfuras special-case handling

### Refactoring Plan (Target Mass: ~84-110)

**Phase 1**: Guard clauses for Sulfuras (-12 to -16 mass)
**Phase 2**: Item name constants (-3 to -5 mass)
**Phase 3**: Quality bounds constants (-2 mass)
**Phase 4**: Quality update methods (-20 to -30 mass)
**Phase 5**: Item type behavior methods (-30 to -40 mass)
**Phase 6**: SellIn threshold constants (-2 mass)

### Current Pattern: Dictionary-Based Dispatch
- Uses Dictionary<string, Action<Item>> for item type dispatch
- Action delegates point to update methods
- Default fallback to UpdateNormalItem for unknown types

### Current Pattern: Strategy Pattern (Implemented)

**Goal**: Refactor to Strategy Pattern with concrete strategy classes for each item type.

**Status**: ✅ Fully implemented

**Benefits**:
1. **Extensibility**: Adding new item types requires only creating a new strategy class and registering it
2. **Testability**: Each strategy can be tested independently
3. **Maintainability**: Each item type's behavior is isolated in its own class
4. **Single Responsibility**: Each strategy class has one clear responsibility
5. **Open/Closed Principle**: Open for extension (new strategies), closed for modification (existing code)

**Proposed Architecture**:
```
IItemUpdateStrategy (interface)
  └── Update(Item item)
  
BaseItemUpdateStrategy (abstract class, optional)
  └── Shared helper methods (DecreaseQuality, IncreaseQuality, etc.)
  
Concrete Strategies:
  - NormalItemStrategy
  - AgedBrieStrategy
  - BackstagePassStrategy
  - SulfurasStrategy
  
ItemUpdateStrategyRegistry (factory/registry)
  └── Maps item names to strategy instances
  
GildedRose
  └── Uses registry to get strategy and delegate Update call
```

**Constraints**:
- Item class cannot be modified (kata constraint)
- Must maintain backward compatibility with existing API
- All existing tests must continue to pass

**Refactoring Steps** (9 steps across 4 phases):

**Phase 1: Create Strategy Infrastructure** (2 steps)
1. Create IItemUpdateStrategy interface - Define `Update(Item item)` method
2. Create BaseItemUpdateStrategy abstract class (optional) - Extract shared helper methods (DecreaseQuality, IncreaseQuality, DecrementSellIn, IsPastSellByDate)

**Phase 2: Extract Strategy Classes** (4 steps)
3. Create NormalItemStrategy class - Implements IItemUpdateStrategy, moves UpdateNormalItem logic
4. Create AgedBrieStrategy class - Implements IItemUpdateStrategy, moves UpdateAgedBrie logic
5. Create BackstagePassStrategy class - Implements IItemUpdateStrategy, moves UpdateBackstagePass and CalculateBackstagePassIncrement logic
6. Create SulfurasStrategy class - Implements IItemUpdateStrategy, moves UpdateSulfuras logic (no-op)

**Phase 3: Create Strategy Registry** (1 step)
7. Create ItemUpdateStrategyRegistry class - Factory/registry pattern to map item names to strategies, provides GetStrategy(string itemName) method, returns default strategy for unknown items

**Phase 4: Refactor GildedRose** (2 steps)
8. Update GildedRose to use strategies - Replace dictionary of Action<Item> with registry, update UpdateQuality to use strategy registry, remove old update methods
9. Clean up GildedRose - Remove unused constants (if strategies handle their own), simplify GildedRose to be a thin orchestrator

**Expected Outcomes**:
- Code Organization: Each item type's behavior is in its own file/class
- Testability: Strategies can be unit tested independently
- Extensibility: New item types require minimal changes (new strategy + registry entry)
- Maintainability: Changes to one item type don't affect others
- Code Mass: May increase slightly due to class overhead, but improves organization

**Implementation Notes**:
- Strategies should be stateless (or use shared state carefully)
- Consider making strategies singletons if they're stateless
- Base class is optional but recommended to reduce duplication
- Registry can use dictionary internally for O(1) lookup
- All constants can move to strategy classes or remain in GildedRose (design decision)

**Testing Strategy**:
- Unit test each strategy class independently
- Integration tests ensure GildedRose works with all strategies
- Existing characterization tests should continue to pass without modification

## Component Relationships
```
GildedRose
  └── Contains: IList<Item>
  └── Operates on: Item properties via UpdateQuality()

Item
  ├── Name (string)
  ├── SellIn (int)
  └── Quality (int)
```

## Critical Implementation Paths
The UpdateQuality method follows this flow:
1. Iterate through all items
2. Adjust quality based on item type (first pass)
3. Decrement SellIn (except for Sulfuras)
4. Apply additional quality changes if past sell-by date

