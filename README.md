# Historical App

Historical App is a gamified history-learning platform built with .NET MAUI and Firebase.  
It includes quiz gameplay, user progression, and leaderboards, plus a small static website for publishing releases.

App builds are distributed from our website (link will be added once hosting is ready).

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
web/                       # Static website (HTML/CSS/JS) for publishing releases (recommended in separate repo for hosting)
HistoricalApp.sln          # Solution file
```

## Tech Stack

- App client: .NET MAUI (.NET 9, C#)
- Web landing site: static HTML/CSS/JS
- Backend services: Firebase Auth + Firebase Realtime Database

## Prerequisites

- .NET 9 SDK
- MAUI workloads installed
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

### 3) Run the web site (for development)

Note: the `web/` site is static HTML/CSS/JS. For easiest hosting, it can live in a separate repo that deploys to Cloudflare Pages or Firebase Hosting.

From repo root:

```bash
npm run web:start
```

Build for production:

```bash
npm run web:build
```

Output is generated in `web/dist/web`.

#### Deploying `web/` to Cloudflare Pages

Cloudflare Pages serves static build output. Recommended Pages settings:

- **Project root**: `web`
- **Build command**: `npm ci && npm run build`
- **Build output directory**: `dist/web/browser`

## Configuration Notes

- Firebase API key and endpoints are currently configured in source files.
- For production, move secrets and environment-specific values to safer configuration patterns.
- Update release metadata/links used by the web site once APK/installer hosting is ready.

## Useful Commands

```bash
# Build Android app
dotnet build HistoricalApp/HistoricalApp.csproj -f net9.0-android -c Debug

# Run web dev server
npm run web:start

# Build web site
npm run web:build
```

## Roadmap Ideas

- Add release pipeline (build + publish APK/installer + update web links)
- Add screenshots/video preview to the release page
- Add CI checks for MAUI and Angular builds
- Externalize Firebase config per environment

## License

See `LICENSE.txt`.
