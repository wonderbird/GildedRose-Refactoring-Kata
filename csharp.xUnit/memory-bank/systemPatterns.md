# System Patterns

## Architecture
Object-oriented design using Strategy pattern with Template Method:
- **Item**: Data class holding item properties (Name, SellIn, Quality)
- **GildedRose**: Orchestrator class delegating to strategies
- **IUpdateStrategy**: Interface defining update behavior contract
- **BaseUpdateStrategy**: Abstract base class with shared constants and helpers
- **Concrete Strategies**: NormalItemStrategy, AgedBrieStrategy, BackstagePassStrategy, ConjuredItemStrategy, SulfurasStrategy
- **IStrategySelector**: Interface for selecting appropriate strategy
- **NameBasedStrategySelector**: Selects strategy based on item name

## Key Technical Decisions
- GildedRose operates on a list of items passed to constructor
- UpdateQuality delegates to strategies for item-specific logic
- Strategy selection based on item name (encapsulated in selector)
- BaseUpdateStrategy eliminates duplication via Template Method pattern
- Each strategy inherits shared helpers from base class
- Strategies override UpdateItem to implement item-specific logic
- No return value from UpdateQuality (void method)
- Item properties are mutable (get/set)
- Strategies mutate items in place

## Design Patterns in Use
**Strategy Pattern** (primary pattern):
- IUpdateStrategy interface defines the contract for all item update behaviors
- Each item type has its own strategy implementation
- GildedRose delegates to strategies via IStrategySelector
- Eliminates conditional logic for type dispatch
- Easy to add new item types without modifying existing code (Open/Closed Principle)

**Template Method Pattern** (base class):
- BaseUpdateStrategy provides shared constants (MaxQuality, MinQuality)
- BaseUpdateStrategy provides shared helper methods (IsPastSellByDate, IsAtMaxQuality, IsAtMinQuality, DecrementSellIn, IncreaseQuality, DecreaseQuality)
- Concrete strategies inherit and override UpdateItem method
- Eliminates ~75 lines of duplicated code (40% reduction)
- Each strategy contains only item-specific logic

**Factory Pattern** (selector):
- NameBasedStrategySelector acts as a strategy factory
- Maps item names to strategy instances
- Returns appropriate strategy for each item
- Provides default strategy for unknown item types

## Code Complexity Analysis (APP)

Using Absolute Priority Premise (APP) mass values to identify refactoring opportunities:

### Current Mass Distribution (After R2.2 - FINAL)
- **Assignments (Mass 6)**: 3 total (reduced from 15) - down 80%!
  - Conjured item quality adjustments: `item.Quality = Math.Max(0, item.Quality - 2)` (2 occurrences)
  - Backstage pass quality reset: `item.Quality = 0` (1 occurrence)
  - ~~Simple ±1 operations~~ (CONVERTED in R1.1): Now use `++`/`--` in helper methods (10 converted)
  - ~~Peculiar quality reset~~ (REMOVED in R1.2): Changed to `item.Quality = 0` (now Constant, Mass 1)
  - ~~Loop variable i and increment~~ (REMOVED in R3.1): Changed to foreach (2 assignments eliminated)
  - ~~SellIn decrements~~ (EXTRACTED in R2.1): Now use DecrementSellIn() method (4 invocations)
- **Conditionals (Mass 4)**: 6 total (reduced from 17) - down 65%!
  - Boundary checks moved to helper methods: 2 in DecreaseQuality, 2 in IncreaseQuality (eliminated 13 duplicates)
  - ~~SellIn checks~~ (EXTRACTED in R2.2): `item.SellIn < 0` now IsPastSellByDate() (4 invocations)
  - Type dispatch in UpdateQuality (4 occurrences)
- **Loop (Mass 5)**: 1 foreach loop in UpdateQuality (no assignment, cleaner)
- **Invocations (Mass 2)**: 11 calls total (DecreaseQuality, IncreaseQuality, DecrementSellIn) - explicit intent

### Identified Duplication Patterns (ALL ELIMINATED!)
1. ~~**SellIn Decrement**~~ (ELIMINATED in R2.1): Was duplicated in 4 Update methods - now centralized in DecrementSellIn()
2. ~~**After-Sell-By-Date Check**~~ (ELIMINATED in R2.2): Was duplicated `if (item.SellIn < 0)` in 4 methods - now IsPastSellByDate()
3. ~~**Quality Adjustment with Boundary**~~ (ELIMINATED in R1.3): Was 13 occurrences - now in helper methods

All planned APP refactorings complete! Code quality significantly improved:
- ~~Extract magic number constants~~ (COMPLETED in R4.1): Quality bounds and backstage tier boundaries now named constants
- ~~Extract after-sell-by-date check~~ (COMPLETED in R2.2): Now uses IsPastSellByDate() helper
- Consider Strategy pattern for polymorphic dispatch (long-term, defer unless needed - current code is clear)

## Component Relationships
```
GildedRose
  ├── Contains: IList<Item>
  └── Uses: IStrategySelector
      └── Returns: IUpdateStrategy implementations
          ├── NormalItemStrategy
          ├── AgedBrieStrategy
          ├── BackstagePassStrategy
          ├── ConjuredItemStrategy
          └── SulfurasStrategy

Item
  ├── Name (string)
  ├── SellIn (int)
  └── Quality (int)
```

## Critical Implementation Paths
The UpdateQuality method follows this flow:
1. Iterate through all items
2. For each item:
   a. Ask selector for appropriate strategy
   b. Delegate to strategy's UpdateItem method
3. Each strategy encapsulates its own logic:
   - Quality adjustments (with boundary checks)
   - SellIn decrement
   - Post-sell-by-date adjustments

