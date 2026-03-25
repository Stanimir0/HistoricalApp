# Historical App

Historical App is a gamified history-learning platform built with .NET MAUI and Firebase.  
It includes quiz gameplay, user progression, and leaderboards, plus a small static website for publishing releases.

App builds are distributed only through the official website:
- https://historical-web.site

Website source repository:
- https://github.com/Stanimir0/historical-web
- can be downloaded from here https://historical-web.site

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

## Configuration Notes

- Firebase API key and endpoints are currently configured in source files.
- For production, move secrets and environment-specific values to safer configuration patterns.
- Update release metadata/links used by the web site once APK/installer hosting is ready.

## Roadmap Ideas

- Add release pipeline (build + publish APK/installer + update web links)
- Add screenshots/video preview to the release page
- Add CI checks for MAUI and Angular builds
- Externalize Firebase config per environment

## License

See `LICENSE.txt`.
