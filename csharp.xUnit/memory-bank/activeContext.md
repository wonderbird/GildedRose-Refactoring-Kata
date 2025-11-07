# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added ninth test: `BackstagePasses_IncreaseQualityByTwo_TenDaysBeforeConcert`
- ✅ Test verifies Backstage passes quality increases by 2 when SellIn = 10
- ✅ All tests passing (9 tests total)
- ✅ Backstage passes second tier coverage (10-6 days before concert)
- 📊 Mutation testing: 42% score (up from 38% - continued progress!)
  - 42 mutants killed (was 38) - +4 more mutants killed!
  - 10 mutants survived (unchanged)
  - 48 mutants no coverage (was 52) - 4 fewer uncovered

## Next Steps
1. Continue Backstage passes tiered behavior:
   - Test within 10-6 days range (e.g., SellIn = 8 or 6)
   - 5-1 days before concert: quality increases by 3
   - After concert (SellIn < 0): quality drops to 0
2. Sulfuras (legendary item) behavior
3. Edge cases (quality bounds, etc.)

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

