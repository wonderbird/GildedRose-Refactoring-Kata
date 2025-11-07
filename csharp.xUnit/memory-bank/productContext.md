# Product Context

## Purpose
The Gilded Rose is a classic refactoring kata that simulates an inventory management system for a shop. The shop sells various items that degrade in quality over time according to complex business rules.

## Problem Being Solved
The existing UpdateQuality method is a legacy implementation with deeply nested conditionals that is difficult to understand, test, and maintain. We need to:
- Understand the existing behavior through comprehensive tests
- Ensure the tests are effective using mutation testing
- Refactor the code to be more maintainable while preserving behavior

## How It Should Work
Items have three properties:
- **Name**: Identifies the item type
- **SellIn**: Days remaining to sell the item
- **Quality**: Value/quality of the item (0-50 range, with some exceptions)

Different item types have different quality degradation rules:
- Normal items: Quality decreases by 1 per day, by 2 after sell-by date
- Aged Brie: Quality increases over time
- Backstage passes: Quality increases, with accelerated increases near concert date, drops to 0 after
- Sulfuras: Legendary item that never changes

## User Experience Goals
For developers working on this code:
- Clear, readable tests that document behavior
- Confidence in refactoring through comprehensive test coverage
- Measurable test effectiveness through mutation testing

