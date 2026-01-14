# CV Website - Dual Tech Stack

A modern, professional CV website built with two separate tech stacks: **C# .NET** and **Angular**. Features a sleek dark theme with sidebar navigation and smooth animations. Switch between implementations using the tech stack buttons in the sidebar.

## Project Structure

```
cv-website/
├── dotnet-cv/          # C# .NET MVC implementation (Purple theme)
│   └── DotNetCV/
│       ├── wwwroot/
│       │   ├── css/
│       │   └── images/  # Place profile.jpg here
│       └── Views/
└── angular-cv/         # Angular implementation (Red theme)
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

### Design
- Modern dark theme with sleek animations
- Fixed sidebar navigation with smooth scrolling
- Full-height hero section with profile display
- Tech-specific color schemes:
  - .NET: Purple/Violet gradients (#667eea, #764ba2)
  - Angular: Red gradients (#dd0031, #c3002f)
- Animated background effects and hover states
- Fully responsive design

### Navigation
- Fixed sidebar with section links
- Tech stack switcher in sidebar
- Smooth scroll to sections
- Social media links (customizable)

### Content Sections
- Hero section with profile circle
- About Me
- Experience (Timeline layout)
- Education & Certifications (Card grid)
- Technical Skills (Category grid)
- Contact Information (Card grid)

## Customization

### Adding Your Profile Picture

1. Save your profile picture as `profile.jpg`
2. Place it in:
   - **.NET**: `dotnet-cv/DotNetCV/wwwroot/images/profile.jpg`
   - **Angular**: `angular-cv/angular-cv/src/assets/images/profile.jpg`
3. Update the CSS to use the image instead of initials:
   ```css
   .profile-circle {
     background-image: url('/images/profile.jpg'); /* .NET */
     /* or */
     background-image: url('assets/images/profile.jpg'); /* Angular */
   }
   .logo {
     background-image: url('/images/profile.jpg'); /* Same for sidebar logo */
   }
   ```

### Updating Your Information

1. Edit content in:
   - **.NET**: `dotnet-cv/DotNetCV/Views/Home/Index.cshtml`
   - **Angular**: `angular-cv/angular-cv/src/app/app.component.html`

2. Modify styling:
   - **.NET**: `dotnet-cv/DotNetCV/wwwroot/css/site.css`
   - **Angular**: `angular-cv/angular-cv/src/app/app.component.css`

### Customizing Social Links

Update the social links in the sidebar footer:
- LinkedIn: Replace `#` with your LinkedIn profile URL
- GitHub: Replace `#` with your GitHub profile URL

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
