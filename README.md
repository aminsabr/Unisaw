# Unisaw.CLI

Command-line tool for calculations and analysis.  
The core logic is in `Unisaw.Core` and unit tests are in `Unisaw.Tests`.

## Prerequisites
- .NET SDK 8.0 or higher  
  Check with: `dotnet --info`

## Project Structure
```
Unisaw.CLI.sln
├─ Unisaw.Core/        # Core logic and classes
├─ Unisaw.CLI/         # CLI application
└─ Unisaw.Tests/       # Unit tests (xUnit)
```

## Build and Run
```bash
dotnet restore
dotnet build
dotnet run --project .\Unisaw.CLI\Unisaw.CLI.csproj
```

## Run Tests
```bash
dotnet test
```

## Branching Guidelines
- `main`: stable branch  
- `feature/<name>`: new feature development
