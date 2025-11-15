# Product Context

## Why This Project Exists
The Gilded Rose refactoring kata is a classic exercise designed to practice refactoring legacy code while maintaining behavior. It teaches how to improve code quality incrementally using TDD.

## Problems It Solves
- Demonstrates refactoring techniques on real-world messy code
- Practices TDD discipline (red-green-refactor cycles)
- Shows how to introduce design patterns safely
- Teaches incremental improvement without breaking functionality

## How It Should Work
The Gilded Rose shop updates item quality and sell-in values daily according to specific rules:
- Regular items: Quality decreases by 1, doubles after sell date
- Aged Brie: Quality increases, continues after sell date
- Backstage passes: Quality increases based on sell date proximity, drops to 0 after concert
- Sulfuras: Legendary item, never changes
- Conjured items: Quality decreases twice as fast (mentioned but not fully implemented)

## User Experience Goals
- Code should be easy to understand
- Adding new item types should be straightforward
- Tests should clearly document expected behavior
- Refactoring should be safe and incremental

