# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added tenth test: `BackstagePasses_IncreaseQualityByThree_FiveDaysBeforeConcert`
- ✅ Test verifies Backstage passes quality increases by 3 when SellIn = 5
- ✅ All tests passing (10 tests total)
- ✅ Backstage passes third tier coverage (5-1 days before concert)
- 📊 Mutation testing: 45% score (up from 42% - steady progress!)
  - 45 mutants killed (was 42) - +3 more mutants killed!
  - 11 mutants survived (was 10) - +1 survivor
  - 44 mutants no coverage (was 48) - 4 fewer uncovered

## Next Steps
1. Complete Backstage passes behavior:
   - After concert (SellIn < 0): quality drops to 0
2. Sulfuras (legendary item) behavior
3. Edge cases (quality bounds, backstage passes at quality 50, etc.)

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

