# Historical App

Historical App is a gamified history-learning platform built with .NET MAUI and Firebase.  
It includes quiz gameplay, user progression, and leaderboards, plus a small static website for publishing releases.

App builds are distributed only through the official website:
- https://historical-web.site

Website source repository:
- https://github.com/Stanimir0/historical-web


## Features

- Firebase authentication (register/login)
- Quiz categories (Battles, Events, Characters)
- Scoring, XP, and rank progression
- Global leaderboard support
- Admin tools for quiz management
- Cross-platform targets: Android,Windows

## Project Structure

```text
HistoricalApp/             # .NET MAUI app
HistoricalApp.Tests/       # Unit tests
HistoricalApp.sln          # Solution file
```

## Tech Stack

- App client: .NET MAUI (.NET 8, C#)
- Web landing site: static HTML/CSS/JS
- Backend services: Firebase Auth + Firebase Realtime Database

## Prerequisites

- .NET 8 SDK
- MAUI workloads installed
- Android/Win workloads and tooling as needed (Android SDK)


## Roadmap Ideas

- Add time based quizes
- Add PVP Quizes where 2 players compete who will score more points for an amount of time
- Add more cosmetics and perks to the shop


## License

See `LICENSE.txt`.
