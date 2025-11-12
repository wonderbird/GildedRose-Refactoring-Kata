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

Future patterns under consideration:
- Strategy pattern for item type behaviors
- Dictionary-based type dispatch
- Value object pattern for quality calculations

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

