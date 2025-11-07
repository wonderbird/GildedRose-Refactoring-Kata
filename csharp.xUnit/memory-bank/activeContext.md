# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added sixth test: `AgedBrie_IncreaseQualityFaster_AfterSellByDate`
- ✅ Test verifies Aged Brie quality increases by 2 per day after sell-by date
- ✅ All tests passing (6 tests total)
- ✅ Aged Brie behavior well-covered (increase before/after sell-by date)
- 📊 Mutation testing: 32% score (up from 28%)
  - 32 mutants killed (was 28) - +4 more mutants killed
  - 10 mutants survived (unchanged)
  - 58 mutants no coverage (was 62) - 4 fewer uncovered

## Next Steps
1. Continue testing quality boundaries:
   - Aged Brie: quality cannot exceed 50 (upper boundary)
   - Then move to Backstage passes behavior
   - Backstage passes behavior
   - Sulfuras (legendary item) behavior
   - Edge cases (quality bounds, etc.)

## Active Decisions
- Following strict TDD: red-green-refactor with commits after each phase
- Starting with simplest possible test (SellIn decrease)
- Building up test coverage incrementally before any refactoring
- Running mutation tests periodically to measure progress and identify gaps
- Focusing on normal item behavior first before moving to special items

## Important Patterns
- Arrange-Act-Assert test structure
- Descriptive test names: `{ItemType}_{Behavior}_{Condition}`
- Testing one behavior per test
- Using List<Item> for test arrangement

