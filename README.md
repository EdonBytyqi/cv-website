# CV Website - Dual Tech Stack

A modern, professional CV website built with two separate tech stacks: **C# .NET** and **Angular**. The two implementations are independent; the Angular version is the primary, redesigned site.

## Project Structure

```
cv-website/
├── dotnet-cv/          # C# .NET MVC implementation (Purple theme)
│   └── DotNetCV/
│       ├── wwwroot/
│       │   ├── css/
│       │   └── images/  # Place profile.jpg here
│       └── Views/
└── angular-cv/         # Angular implementation (primary site)
    └── angular-cv/
        └── src/
            ├── app/
            └── assets/
                └── images/  # Place profile.jpg here
```

## Running the .NET Version

1. Navigate to the .NET project:
   ```bash
   cd dotnet-cv/DotNetCV
   ```

2. Run the application:
   ```bash
   dotnet run
   ```

3. Open your browser and go to: `http://localhost:5000`

## Running the Angular Version

1. Navigate to the Angular project:
   ```bash
   cd angular-cv/angular-cv
   ```

2. Install dependencies (first time only):
   ```bash
   npm install
   ```

3. Run the development server:
   ```bash
   ng serve
   ```

4. Open your browser and go to: `http://localhost:4200`

## Features

### Angular version
- Welcome animation: a hand-drawn "hello" writes itself on first visit, then dissolves into the page. Plays once per browser session, can be skipped with a click or any key, and is skipped entirely for `prefers-reduced-motion`
- Apple.com-inspired look: `#f5f5f7` and white bands with contrasting cards, `#1d1d1f` text, blue pill buttons, orange "Current" label, pure black in dark mode; SF on Apple devices, Inter elsewhere
- apple.com layout: a 44px translucent global nav (name left, section links centred, LinkedIn / GitHub / Email icons right), a centred hero with two pill buttons, and full-width bands alternating gray and white
- Light and dark themes that follow the system setting, built on CSS tokens in `src/styles.css`
- Motion details: press feedback on buttons, tooltips that delay the first time and open instantly after that, a blurred crossfade on the Copy → Copied button, active-section indicator, staggered page entrance
- Mobile polish: safe-area insets, per-scheme `theme-color`, no tap highlight or tap delay
- Print stylesheet for a clean one-column paper CV

### .NET version
- Dark purple theme with fixed sidebar navigation and a full-height hero

## Customization

### Updating your information

- **Angular**: edit the data in `angular-cv/angular-cv/src/app/services/cv.service.ts`. The phone number is optional and only shown when set; keep personal numbers out of the public repo.
- **.NET**: edit `dotnet-cv/DotNetCV/Services/CvService.cs` and the views in `dotnet-cv/DotNetCV/Views/Home/`.

### Profile picture

- **Angular**: `angular-cv/angular-cv/src/assets/images/profile.jpg`
- **.NET**: `dotnet-cv/DotNetCV/wwwroot/images/profile.jpg`

### Styling

- **Angular**: tokens (colors, easing, durations) in `src/styles.css`, layout in `src/app/app.component.css`
- **.NET**: `dotnet-cv/DotNetCV/wwwroot/css/site.css`

## Tech Stack

### .NET Version
- ASP.NET Core 9.0
- MVC Pattern
- Bootstrap 5
- Razor Views

### Angular Version
- Angular 19
- TypeScript
- CSS3
- Standalone Components
