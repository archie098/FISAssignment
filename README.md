# README

## Overview
This is a test automation framework built using C#, Playwright, NUnit, and Extent Reports


## Tech Stack

- .NET 8
- Playwright (.NET)
- NUnit
- Extent Reports
- Newtonsoft.Json


## Prerequisites

- .NET SDK 8.0 or later
- Visual Studio 2022

Verify installation:

#bash
#dotnet --version


## Project Setup

### 1. Restore NuGet Packages

dotnet restore

### 2. Install Playwright Browsers

playwright install


## Configuration

Update application settings in:

TestData/appSettings.json

## Running Tests

### Execute All Tests

dotnet test

### Execute Specific Test

dotnet test --filter "FullyQualifiedName~AddBookToCartTests"


## Reporting

Extent Report is generated automatically under:

Reports/

Includes:
- Test execution status
- Test steps
- Failure details
- Screenshots
- Execution summary


## Screenshots

Failure screenshots are captured automatically and stored under:

Screenshots/


## Framework Design Highlights

- Layered architecture with clear separation of concerns.
- Page Object Model (POM) implementation.
- Configuration-driven execution using JSON files.
- Reusable Base and Utility layers.
- Extent Reports integration.
- API layer for future UI + API automation support.
- Follows SOLID principles, especially Single Responsibility Principle (SRP).
- Easily extendable for parallel and cross-browser execution.

## Author

**Archisman Chatterjee**
