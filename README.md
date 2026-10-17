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
- Editorial two-column layout: sticky intro and section nav on desktop, single column with a sticky nav strip on phones
- Light and dark themes that follow the system setting, built on CSS tokens in `src/styles.css`
- Active-section highlighting in the nav while scrolling
- Restrained motion: a short staggered entrance on load, press feedback, hover effects only on precise pointers, and a gentler variant for `prefers-reduced-motion`
- Mobile polish: safe-area insets, per-scheme `theme-color`, no tap highlight or tap delay
- Print stylesheet for a clean one-column paper CV
- "Copy email" button with confirmation

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
