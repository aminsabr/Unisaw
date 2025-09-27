# Unisaw.CLI

[![.NET CI](https://github.com/aminsabr/Unisaw/actions/workflows/workflows/dotnet.yml/badge.svg)](https://github.com/aminsabr/Unisaw/actions/workflows/workflows/dotnet.yml)

Simple command-line tool for calculations and analysis.  
Core logic lives in `Unisaw.Core`, CLI in `Unisaw.CLI`, and tests in `Unisaw.Tests` (xUnit).

## Prerequisites
- .NET SDK **8.0+** (check with `dotnet --info`)

## Project Structure
```
Unisaw.CLI.sln
├─ Unisaw.Core/        # Core logic and classes
├─ Unisaw.CLI/         # CLI application
└─ Unisaw.Tests/       # Unit tests (xUnit)
```

## Quickstart
```bash
dotnet restore
dotnet build
# Windows
dotnet run --project .\Unisaw.CLI\Unisaw.CLI.csproj
# macOS/Linux
dotnet run --project ./Unisaw.CLI/Unisaw.CLI.csproj
```

## CLI: `cuts` Command
Calculates how many pieces fit into a stock length considering saw kerf (kerf is applied **between** cuts, not after the last one).

**Syntax**
```bash
cuts --stock <number> --piece <number> [--kerf <number>]
# aliases: -s, -p, -k
```

**Example**
```bash
dotnet run --project .\Unisaw.CLI\Unisaw.CLI.csproj -- cuts -s 2400 -p 400 -k 3
```

## Tests
```bash
dotnet test
```

## CI
GitHub Actions workflow at `.github/workflows/dotnet.yml` restores, builds, and tests on every push/PR.

## Versioning
Latest tagged release: `v0.1.0`

## License
MIT
