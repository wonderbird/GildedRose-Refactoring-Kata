# Technical Context

## Technologies Used
- **Language**: C# (.NET 8.0)
- **Testing Framework**: xUnit
- **Test Verification**: Verify.Xunit (approval testing)
- **Build Tool**: .NET SDK (dotnet CLI)

## Development Setup
- Solution file: `GildedRose.sln`
- Production project: `GildedRose/`
- Test project: `GildedRoseTests/`

## Technical Constraints
- Cannot modify the `Item` class (it's part of the kata constraints)
- Must maintain backward compatibility with existing code
- All refactoring must be test-driven

## Dependencies
- xUnit for unit testing
- Verify.Xunit for approval testing
- .NET 8.0 runtime

## Tool Usage Patterns
- Build: `dotnet build GildedRose.sln -c Debug`
- Test: `dotnet test`
- Run program: `GildedRose/bin/Debug/net8.0/GildedRose [days]`

## Code Quality Tools
- Mutation testing (Stryker) mentioned in rules
- Code coverage collection available

