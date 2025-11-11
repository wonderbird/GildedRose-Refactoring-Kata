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

## Code Complexity Analysis (APP)

Using Absolute Priority Premise (APP) mass values to identify refactoring opportunities:

### Current Mass Distribution
- **Assignments (Mass 6)**: 16 total - highest priority for reduction
  - Quality adjustments: `item.Quality = item.Quality ± 1` (14 occurrences)
  - SellIn decrements: `item.SellIn = item.SellIn - 1` (4 occurrences)
  - Peculiar quality reset: `item.Quality = item.Quality - item.Quality` (1 occurrence, line 137)
- **Conditionals (Mass 4)**: 17 total
  - Boundary checks: `!IsAtMinQuality(item)`, `!IsAtMaxQuality(item)` (9 occurrences)
  - SellIn checks: `item.SellIn < 0` (4 occurrences)
  - Type dispatch in UpdateQuality (4 occurrences)
- **Loop (Mass 5)**: 1 for loop with index variable in UpdateQuality

### Identified Duplication Patterns
1. **SellIn Decrement** - duplicated in 4 Update methods (lines 87, 105, 133, 148)
2. **After-Sell-By-Date Check** - duplicated pattern `if (item.SellIn < 0)` in 4 methods
3. **Quality Adjustment with Boundary** - 13 occurrences of check-then-adjust pattern

Future refactoring opportunities:
- Extract quality adjustment methods (DecreaseQuality, IncreaseQuality) to reduce assignment mass
- Extract SellIn decrement to eliminate duplication
- Replace for loop with foreach to reduce assignment count
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

