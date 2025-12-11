# Project Brief

## Overview
This is the Gilded Rose Refactoring Kata implemented in C# with xUnit testing framework.

## Goals
- Create comprehensive unit tests to characterize the existing GildedRose behavior
- Enable mutation testing with Stryker to assess test quality
- Refactor the legacy UpdateQuality method while maintaining behavior
- Follow strict TDD practices (red-green-refactor)

## Scope
- GildedRose class with UpdateQuality method
- Item class with Name, SellIn, and Quality properties
- Unit tests using xUnit framework
- Mutation testing using Stryker.NET

## Constraints
- Must not modify the Item class (stated in kata rules)
- Must maintain backward compatibility with existing behavior
- All commits must follow conventional commit format
- Must use strict TDD approach with git commits after each phase

