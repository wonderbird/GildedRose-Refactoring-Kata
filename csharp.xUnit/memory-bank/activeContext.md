# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added fourth test: `NormalItem_QualityNeverNegative`
- ✅ Test verifies quality boundary: quality stays at 0, never goes negative
- ✅ All tests passing (4 tests total)
- ✅ Normal item behavior comprehensively covered: SellIn, quality degradation, and lower boundary
- 📊 Mutation testing: 22% score (up from 21%)
  - 22 mutants killed (was 21) - boundary test killed 1 more mutant
  - 10 mutants survived (was 11) - 1 fewer surviving
  - 68 mutants no coverage (unchanged - still need special item tests)

## Next Steps
1. Begin testing special item types:
   - Aged Brie: quality increases over time
   - Aged Brie: quality increases faster after sell-by date
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

