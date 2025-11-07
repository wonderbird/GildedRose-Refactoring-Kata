# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added eighth test: `BackstagePasses_IncreaseQualityByOne_MoreThanTenDaysBeforeConcert`
- ✅ Test verifies Backstage passes quality increases by 1 when SellIn > 10
- ✅ All tests passing (8 tests total)
- ✅ Started Backstage passes coverage (first tier of quality increase)
- 📊 Mutation testing: 38% score (up from 33% - big jump!)
  - 38 mutants killed (was 33) - +5 more mutants killed!
  - 10 mutants survived (was 9) - 1 additional survivor
  - 52 mutants no coverage (was 58) - 6 fewer uncovered

## Next Steps
1. Continue Backstage passes tiered behavior:
   - 10-6 days before concert: quality increases by 2
   - 5-1 days before concert: quality increases by 3
   - After concert (SellIn < 0): quality drops to 0
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

