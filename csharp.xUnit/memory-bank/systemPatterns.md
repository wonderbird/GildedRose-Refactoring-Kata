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
Current implementation uses procedural conditional logic:
- Method extraction pattern - separate methods per item type (UpdateNormalItem, UpdateAgedBrie, etc.)
- Item type discrimination via Name string comparison
- In-place mutation of item properties
- Helper methods for type identification (IsAgedBrie, IsSulfuras, etc.)
- Helper methods for boundary checks (IsAtMaxQuality, IsAtMinQuality)
- Helper methods for common operations (DecreaseQuality, IncreaseQuality, DecrementSellIn)

## Code Complexity Analysis (APP)

Using Absolute Priority Premise (APP) mass values to identify refactoring opportunities:

### Current Mass Distribution (After R2.1)
- **Assignments (Mass 6)**: 3 total (reduced from 15) - down 80%!
  - Conjured item quality adjustments: `item.Quality = Math.Max(0, item.Quality - 2)` (2 occurrences)
  - Backstage pass quality reset: `item.Quality = 0` (1 occurrence)
  - ~~Simple ±1 operations~~ (CONVERTED in R1.1): Now use `++`/`--` in helper methods (10 converted)
  - ~~Peculiar quality reset~~ (REMOVED in R1.2): Changed to `item.Quality = 0` (now Constant, Mass 1)
  - ~~Loop variable i and increment~~ (REMOVED in R3.1): Changed to foreach (2 assignments eliminated)
  - ~~SellIn decrements~~ (EXTRACTED in R2.1): Now use DecrementSellIn() method (4 invocations)
- **Conditionals (Mass 4)**: 6 total (reduced from 17) - down 65%!
  - Boundary checks moved to helper methods: 2 in DecreaseQuality, 2 in IncreaseQuality (eliminated 13 duplicates)
  - SellIn checks: `item.SellIn < 0` (4 occurrences in Update methods)
  - Type dispatch in UpdateQuality (4 occurrences)
- **Loop (Mass 5)**: 1 foreach loop in UpdateQuality (no assignment, cleaner)
- **Invocations (Mass 2)**: 11 calls total (DecreaseQuality, IncreaseQuality, DecrementSellIn) - explicit intent

### Identified Duplication Patterns
1. ~~**SellIn Decrement**~~ (ELIMINATED in R2.1): Was duplicated in 4 Update methods - now centralized in DecrementSellIn()
2. **After-Sell-By-Date Check** - duplicated pattern `if (item.SellIn < 0)` in 4 methods
3. ~~**Quality Adjustment with Boundary**~~ (ELIMINATED in R1.3): Was 13 occurrences - now in helper methods

Future refactoring opportunities:
- Extract magic number constants (quality bounds, backstage tier boundaries)
- Consider extracting after-sell-by-date adjustment pattern
- Consider Strategy pattern for polymorphic dispatch (long-term, defer unless needed)

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

