# Deployment Guide - CV Website

## Security Improvements Applied ✅

### .NET Application
- ✅ Security headers (X-Frame-Options, X-Content-Type-Options, CSP, etc.)
- ✅ HSTS enabled for production
- ✅ HTTPS redirection
- ✅ No hardcoded secrets
- ✅ Production-ready error handling

### Angular Application
- ✅ Security meta tags
- ✅ SEO optimizations (title, description)
- ✅ Production-ready configuration

### General
- ✅ Proper .gitignore (prevents committing sensitive files)
- ✅ Removed hardcoded localhost URLs

---

## Deployment Options

### Option 1: Azure (Recommended for Full Stack)

**Cost:** ~$10-20/month for App Service

#### Steps:

1. **Create Azure Account** (free tier available)

2. **Deploy .NET App:**
   ```bash
   cd dotnet-cv/DotNetCV
   dotnet publish -c Release -o ./publish

   # Install Azure CLI
   az login
   az webapp up --name your-app-name --resource-group your-rg --runtime "DOTNETCORE:9.0"
   ```

3. **Deploy Angular App:**
   ```bash
   cd angular-cv/angular-cv
   npm run build

   # Create Static Web App
   az staticwebapp create --name your-angular-app --resource-group your-rg
   ```

4. **Configure Custom Domain:**
   - Go to Azure Portal
   - Custom domains → Add custom domain
   - Add DNS records at your domain registrar

---

### Option 2: Vercel (Angular) + Azure (. NET)

**Cost:** Free for Angular, ~$10/month for .NET

#### Angular on Vercel (FREE):

1. Install Vercel CLI:
   ```bash
   npm install -g vercel
   ```

2. Deploy:
   ```bash
   cd angular-cv/angular-cv
   vercel --prod
   ```

3. Add custom domain in Vercel dashboard

#### .NET on Azure:
Follow Azure steps above for .NET only

---

### Option 3: DigitalOcean Droplet (Full Control)

**Cost:** $6/month (basic droplet)

#### Setup Steps:

1. **Create Droplet** (Ubuntu 22.04)

2. **Install Prerequisites:**
   ```bash
   # SSH into droplet
   ssh root@your-droplet-ip

   # Install .NET
   wget https://dot.net/v1/dotnet-install.sh
   chmod +x dotnet-install.sh
   ./dotnet-install.sh --channel 9.0

   # Install Nginx
   apt update
   apt install nginx -y
   ```

3. **Deploy .NET App:**
   ```bash
   # On your local machine, build and copy
   cd dotnet-cv/DotNetCV
   dotnet publish -c Release -o ./publish
   scp -r ./publish root@your-droplet-ip:/var/www/dotnet-cv

   # On droplet, create systemd service
   nano /etc/systemd/system/dotnet-cv.service
   ```

   Add this content:
   ```ini
   [Unit]
   Description=DotNet CV App

   [Service]
   WorkingDirectory=/var/www/dotnet-cv
   ExecStart=/root/.dotnet/dotnet /var/www/dotnet-cv/DotNetCV.dll
   Restart=always
   RestartSec=10
   KillSignal=SIGINT
   SyslogIdentifier=dotnet-cv
   User=www-data
   Environment=ASPNETCORE_ENVIRONMENT=Production
   Environment=ASPNETCORE_URLS=http://localhost:5000

   [Install]
   WantedBy=multi-user.target
   ```

   ```bash
   systemctl enable dotnet-cv
   systemctl start dotnet-cv
   ```

4. **Deploy Angular App:**
   ```bash
   # On local machine
   cd angular-cv/angular-cv
   npm run build

   # Copy to droplet
   scp -r ./dist/angular-cv/browser root@your-droplet-ip:/var/www/angular-cv
   ```

5. **Configure Nginx:**
   ```bash
   nano /etc/nginx/sites-available/cv-website
   ```

   Add:
   ```nginx
   server {
       listen 80;
       server_name yourdomain.com www.yourdomain.com;

       # Angular app
       location /angular {
           alias /var/www/angular-cv;
           try_files $uri $uri/ /angular/index.html;
       }

       # .NET app
       location /dotnet {
           proxy_pass http://localhost:5000;
           proxy_http_version 1.1;
           proxy_set_header Upgrade $http_upgrade;
           proxy_set_header Connection keep-alive;
           proxy_set_header Host $host;
           proxy_cache_bypass $http_upgrade;
       }

       # Default to Angular
       location / {
           root /var/www/angular-cv;
           try_files $uri $uri/ /index.html;
       }
   }
   ```

   ```bash
   ln -s /etc/nginx/sites-available/cv-website /etc/nginx/sites-enabled/
   nginx -t
   systemctl restart nginx
   ```

6. **Setup SSL with Let's Encrypt:**
   ```bash
   apt install certbot python3-certbot-nginx -y
   certbot --nginx -d yourdomain.com -d www.yourdomain.com
   ```

7. **Configure DNS:**
   - Add A record: `yourdomain.com` → your droplet IP
   - Add A record: `www.yourdomain.com` → your droplet IP

---

## Pre-Deployment Checklist

### Before Deploying:

- [ ] Update personal information (LinkedIn, GitHub URLs)
- [ ] Replace placeholder social links in HTML
- [ ] Test both apps locally
- [ ] Run production builds to check for errors
- [ ] Review appsettings.json (no secrets!)
- [ ] Set up environment variables for production
- [ ] Configure proper logging
- [ ] Test on mobile devices

### .NET Specific:
```bash
cd dotnet-cv/DotNetCV
dotnet build -c Release
# Fix any warnings
```

### Angular Specific:
```bash
cd angular-cv/angular-cv
ng build --configuration production
# Check for build errors
```

---

## Domain Configuration

Once you have a domain (e.g., from Namecheap, GoDaddy):

1. **For Static Hosting (Vercel/Netlify):**
   - Add CNAME record: `www` → `cname.vercel-dns.com`
   - Add A record: `@` → Vercel IP (provided in dashboard)

2. **For VPS (DigitalOcean):**
   - Add A record: `@` → your droplet IP
   - Add A record: `www` → your droplet IP

3. **Wait for DNS propagation** (up to 48 hours, usually faster)

---

## Ongoing Security

### Regular Updates:
```bash
# Update .NET packages
dotnet list package --outdated
dotnet add package <PackageName>

# Update Angular packages
npm outdated
npm update
```

### Monitoring:
- Set up uptime monitoring (UptimeRobot - free)
- Configure error logging (Azure Application Insights or Sentry)
- Enable firewall on VPS (ufw for Ubuntu)

### SSL Certificate Renewal:
- Automatic with Let's Encrypt (auto-renews every 90 days)
- Or managed by hosting provider (Vercel, Azure, etc.)

---

## Cost Comparison

| Option | Cost/Month | Pros | Cons |
|--------|-----------|------|------|
| Azure App Service | $10-20 | Easy, managed, auto-scaling | Pricier |
| Vercel + Azure | $10 | Angular free, easy deploys | Split infrastructure |
| DigitalOcean | $6 | Full control, cheapest | Manual setup, maintenance |
| Netlify (Angular only) | $0 | Free, easy | Only static sites |

---

## Need Help?

Reach out if you need assistance with:
- Choosing the right hosting option
- Setting up deployment
- Configuring your domain
- Troubleshooting issues
