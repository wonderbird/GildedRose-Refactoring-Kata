# Gilded Rose starting position in C# xUnit

## Build the project

Use your normal build tools to build the projects in Debug mode.
For example, you can use the `dotnet` command line tool:

``` cmd
dotnet build GildedRose.sln -c Debug
```

## Run the Gilded Rose Command-Line program

For e.g. 10 days:

``` cmd
GildedRose/bin/Debug/net8.0/GildedRose 10
```

## Run all the unit tests

``` cmd
dotnet test
```

## Run mutation tests with Stryker Mutator

If this is the first time you want to run [Stryker Mutator](https://stryker-mutator.io), then [install the dotnet tool dotnet-stryker](https://stryker-mutator.io/docs/stryker-net/getting-started/) first:

``` cmd
dotnet tool restore
```

Once you have installed dotnet-stryker, you can start mutation testing

``` cmd
dotnet stryker
```
