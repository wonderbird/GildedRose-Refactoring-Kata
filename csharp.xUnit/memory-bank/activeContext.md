# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added fifth test: `AgedBrie_IncreaseQuality_BeforeSellByDate`
- ✅ Test verifies Aged Brie quality increases by 1 per day (opposite of normal items)
- ✅ All tests passing (5 tests total)
- ✅ Started covering special item behavior (Aged Brie)
- 📊 Mutation testing: 28% score (up from 22% - significant jump!)
  - 28 mutants killed (was 22) - Aged Brie test killed 6 more mutants!
  - 10 mutants survived (unchanged)
  - 62 mutants no coverage (was 68) - 6 fewer uncovered

## Next Steps
1. Continue testing Aged Brie behavior:
   - Aged Brie: quality increases faster after sell-by date (by 2 per day)
   - Aged Brie: quality cannot exceed 50
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

