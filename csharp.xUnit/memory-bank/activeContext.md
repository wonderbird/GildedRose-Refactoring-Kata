# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added second test: `NormalItem_DecreaseQuality_BeforeSellByDate`
- ✅ Test verifies that normal item quality decreases by 1 per day (before sell-by date)
- ✅ All tests passing (2 tests total)
- 📊 Initial mutation testing: 6% mutation score (129 mutants, 6 killed, 16 survived, 78 no coverage)

## Next Steps
1. Continue adding characterization tests:
   - Normal item quality decrease acceleration after sell-by date (doubles to -2/day)
   - Quality boundary: cannot go below 0
   - Aged Brie behavior
   - Backstage passes behavior
   - Sulfuras (legendary item) behavior
   - Edge cases (quality bounds, etc.)

## Active Decisions
- Following strict TDD: red-green-refactor with commits after each phase
- Starting with simplest possible test (SellIn decrease)
- Building up test coverage incrementally before any refactoring

## Important Patterns
- Arrange-Act-Assert test structure
- Descriptive test names: `{ItemType}_{Behavior}_{Condition}`
- Testing one behavior per test
- Using List<Item> for test arrangement

