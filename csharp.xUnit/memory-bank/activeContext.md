# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added seventh test: `AgedBrie_QualityNeverExceedsFifty`
- ✅ Test verifies Aged Brie quality caps at 50 (upper boundary)
- ✅ All tests passing (7 tests total)
- ✅ Aged Brie behavior fully covered (increase, acceleration, both boundaries)
- 📊 Mutation testing: 33% score (up from 32%)
  - 33 mutants killed (was 32) - +1 more mutant killed
  - 9 mutants survived (was 10) - 1 fewer survivor!
  - 58 mutants no coverage (unchanged - still need Backstage passes, Sulfuras)

## Next Steps
1. Move to Backstage passes (complex behavior with multiple tiers):
   - More than 10 days: quality increases by 1
   - 10-6 days: quality increases by 2
   - 5-1 days: quality increases by 3
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

