# Web

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.2.22.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.

## Deploying to Cloudflare Pages (Angular)

This project is a client-side Angular app. The production build outputs files under `dist/web/browser`, so Cloudflare Pages should use that as the build output directory.

Recommended Cloudflare Pages settings:

- **Source**: your GitHub repo branch (for example `master`)
- **Project root**: `web`
- **Build command**: `npm ci && npm run build`
- **Build output directory**: `dist/web/browser`

After deployment:
- The landing page will be served from `/`.
- `web/public/releases.json` is included in the build and will be available at `/releases.json` (used by `app.component.ts` via `fetch('/releases.json')`).

No SPA routing fallback (rewrite-to-`index.html`) is required right now because this web app doesn’t define additional client routes.
