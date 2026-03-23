# Historical App

Historical App is a gamified history-learning platform built with .NET MAUI and Firebase.  
It includes quiz gameplay, user progression, and leaderboards, plus a new Angular download site for publishing app builds.

## Features

- Firebase authentication (register/login)
- Quiz categories (Battles, Events, Characters)
- Scoring, XP, and rank progression
- Global leaderboard support
- Admin tools for quiz management
- Cross-platform targets: Android, iOS, macOS, Windows

## Project Structure

```text
HistoricalApp/             # .NET MAUI app
HistoricalApp.Tests/       # Unit tests
web/                       # Angular download/landing site
HistoricalApp.sln          # Solution file
```

## Tech Stack

- App client: .NET MAUI (.NET 9, C#)
- Web landing site: Angular
- Backend services: Firebase Auth + Firebase Realtime Database

## Prerequisites

- .NET 9 SDK
- MAUI workloads installed
- Node.js + npm (for the Angular `web/` project)
- Android/iOS workloads and tooling as needed (Android SDK, Xcode on macOS for iOS)

## Getting Started

### 1) Clone and restore

```bash
git clone <your-repo-url>
cd HistoricalApp
dotnet restore HistoricalApp.sln
```

### 2) Run the MAUI app

From Visual Studio/Cursor:
- Open `HistoricalApp.sln`
- Select a target (Android, Windows, etc.)
- Build and run

CLI examples:

```bash
dotnet build HistoricalApp/HistoricalApp.csproj -f net9.0-android
dotnet build HistoricalApp/HistoricalApp.csproj -f net9.0-windows10.0.19041.0
```

### 3) Run the Angular download site

From repo root:

```bash
npm run web:start
```

Build for production:

```bash
npm run web:build
```

Output is generated in `web/dist/web`.

## Configuration Notes

- Firebase API key and endpoints are currently configured in source files.
- For production, move secrets and environment-specific values to safer configuration patterns.
- Update download links in `web/src/app/app.component.ts` once APK/installer hosting is ready.

## Useful Commands

```bash
# Build Android app
dotnet build HistoricalApp/HistoricalApp.csproj -f net9.0-android -c Debug

# Run Angular dev server
npm run web:start

# Build Angular site
npm run web:build
```

## Roadmap Ideas

- Add release pipeline (build + publish APK/installer + update web links)
- Add screenshots/video preview to the download page
- Add CI checks for MAUI and Angular builds
- Externalize Firebase config per environment

## License

See `LICENSE.txt`.
