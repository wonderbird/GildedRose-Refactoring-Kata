# Active Context

## Current Work Focus
Expanding test coverage to characterize all GildedRose behaviors before refactoring.

## Recent Changes
- ✅ Added third test: `NormalItem_DecreaseQualityTwiceAsFast_AfterSellByDate`
- ✅ Test verifies quality decreases by 2 per day after sell-by date (SellIn < 0)
- ✅ All tests passing (3 tests total)
- ✅ Normal item behavior now well-covered (SellIn decrease, quality decrease before/after sell-by)
- 📊 Mutation testing improved: 21% score (up from 6% baseline)
  - 21 mutants killed (was 6) - 3.5x improvement
  - 11 mutants survived (was 16) - 5 fewer surviving
  - 68 mutants no coverage (was 78) - 10 more paths covered

## Next Steps
1. Test quality boundaries for normal items:
   - Quality cannot go below 0
   - Aged Brie behavior
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

