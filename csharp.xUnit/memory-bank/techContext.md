# Technical Context

## Technologies Used
- **Language**: C# (.NET 8.0)
- **Test Framework**: xUnit
- **Mutation Testing**: Stryker.NET (dotnet-stryker)
- **Build Tool**: dotnet CLI
- **Version Control**: Git

## Development Setup
Project structure:
```
csharp.xUnit/
├── GildedRose/              # Main project
│   ├── GildedRose.cs       # Business logic
│   ├── Item.cs             # Data model
│   └── Program.cs          # Console application entry
├── GildedRoseTests/         # Test project
│   └── GildedRoseTest.cs   # xUnit tests
├── GildedRose.sln          # Solution file
└── memory-bank/            # Documentation
```

## Build Commands
- Build: `dotnet build GildedRose.sln -c Debug`
- Test: `dotnet test`
- Run app: `GildedRose/bin/Debug/net8.0/GildedRose <days>`
- Mutation test: `dotnet stryker` (requires `dotnet tool restore` first)

## Technical Constraints
- Target framework: .NET 8.0
- Cannot modify Item class (kata rule)
- Must maintain backward compatibility

## Dependencies
Test project dependencies:
- xUnit (test framework)
- xUnit.runner.visualstudio (test runner)
- Microsoft.NET.Test.Sdk (test platform)
- Verify.Xunit (snapshot testing - installed but not yet used)

## Tool Usage Patterns
Following strict TDD workflow:
1. Write failing test
2. Make test pass with minimal code
3. Refactor while keeping tests green
4. Commit after each green and refactor step
5. Use conventional commits with Co-authored-by trailer

