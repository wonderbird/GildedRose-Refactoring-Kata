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
- Nested if-else statements
- Item type discrimination via Name string comparison
- In-place mutation of item properties

Future refactoring targets:
- Strategy pattern or polymorphism for item type behaviors
- Value object pattern for quality/sellin calculations
- Possibly visitor pattern for update operations

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

