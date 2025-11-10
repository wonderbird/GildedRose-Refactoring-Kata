# Active Context

## Current Work Focus
**Implement "Conjured" items feature** - A new category of item, "Conjured" items, needs to be added to the system. These items degrade in quality twice as fast as normal items.

## Recent Changes
- ✅ RED/GREEN: Test and implementation for quality degradation by 2 before sell-by date
- ✅ RED/GREEN: Test and implementation for quality degradation by 4 after sell-by date

## Next Steps - Implement "Conjured" Items
1.  ✅ Write a failing test for "Conjured" item quality degradation (degrades by 2) before the sell-by date.
2.  ✅ Implement the minimal code required to make the test pass.
3.  ✅ Refactor the implementation while keeping tests green (no refactoring needed - clean implementation).
4.  ✅ Write a failing test for "Conjured" item quality degradation (degrades by 4) after the sell-by date.
5.  ✅ Implement the minimal code required to make the test pass.
6.  Refactor the implementation (if needed).
7.  Add tests for boundary conditions, such as quality never dropping below zero.
8.  Run mutation tests to ensure the new logic is fully covered.

## Active Decisions
- The implementation of "Conjured" items will follow the same strict TDD cycle (Red-Green-Refactor) that was used for the initial refactoring.
- A new set of tests will be created specifically for "Conjured" item behavior.
- The existing refactored structure of `UpdateQuality` will be extended to accommodate the new item type.

## Test Suite Quality Achieved
- **27 passing tests** covering all business logic (including new conjured items)
- **Mutation coverage improving** for conjured items feature (58.88% overall)
- **Self-documenting** with named constants, expressive assertions, and comprehensive documentation
- **Maintainable** with helper methods reducing duplication
- **Accessible** to junior developers with clear explanations

## Important Patterns
- Arrange-Act-Assert test structure
- Descriptive test names: `{ItemType}_{Behavior}_{Condition}`
- Testing one behavior per test
- Using List<Item> for test arrangement

