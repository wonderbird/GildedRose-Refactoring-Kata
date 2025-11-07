# Active Context

## Current Work Focus
Creating the first unit test to enable mutation testing with Stryker.

## Recent Changes
- Replaced the failing `EmptyTest` with `NormalItem_DecreaseSellIn_AfterOneDay`
- Test verifies that a normal item's SellIn property decreases by 1 after calling UpdateQuality()
- Test passes with existing implementation (green phase)

## Next Steps
1. Commit the current test with proper conventional commit format including memory bank
2. Run Stryker mutation tests to verify setup
3. Add more tests to characterize other behaviors:
   - Normal item quality decrease
   - Quality decrease acceleration after sell-by date
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

