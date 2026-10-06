# AutoExercise .NET

Playwright UI automation tests for [automationexercise.com](https://automationexercise.com) — a practice site for automation engineers. Written in C# using NUnit and Microsoft Playwright.

## Tech Stack

| | |
|---|---|
| Language | C# (.NET 10) |
| Test Framework | NUnit 4 |
| Browser Automation | Microsoft Playwright |
| Test Data | Bogus |
| CI | GitHub Actions |

## Project Structure

```
tests/AutoExercise.Tests/
├── Api/            # REST API client for test setup/teardown
├── Components/     # Reusable page components (e.g. HeaderNav)
├── Data/           # Test data models and factory
├── Fixtures/       # Base test classes (BaseUiTest)
├── Pages/          # Page Object Model classes
└── Tests/Ui/       # Test classes
```

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PowerShell (`pwsh`) — required for the Playwright browser installer

## Setup

**1. Restore dependencies and build:**

```bash
dotnet build
```

**2. Install Playwright browsers:**

```bash
pwsh tests/AutoExercise.Tests/bin/Debug/net10.0/playwright.ps1 install --with-deps chromium
```

## Running Tests

```bash
dotnet test --no-build --settings tests/AutoExercise.Tests/.runsettings --logger trx
```

Tests run headless against Chromium by default. The `.runsettings` file controls browser and launch options.

## Configuration

Browser context is configured in `Fixtures/BaseUiTest.cs`:

- **Base URL:** `https://automationexercise.com`
- **Viewport:** 1280 × 800
- **Test ID attribute:** `data-qa`
- **Ad blocking:** requests to Google ad domains are intercepted and aborted

All UI test classes should extend `BaseUiTest` (not `PageTest` directly) to inherit this configuration.

## CI

GitHub Actions runs on every push and pull request: build → install browsers → run tests. See [`.github/workflows/tests.yml`](.github/workflows/tests.yml).
