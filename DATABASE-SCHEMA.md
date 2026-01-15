# Supabase Database Schema for CV Website

## Overview
This document defines the database schema for the CV website using Supabase (PostgreSQL).

---

## Tables

### 1. `about_me`
Stores personal information and bio.

```sql
CREATE TABLE about_me (
    id SERIAL PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    title VARCHAR(255) NOT NULL,
    bio TEXT NOT NULL,
    profile_image_url VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Only one record should exist
CREATE UNIQUE INDEX idx_about_me_singleton ON about_me ((1));
```

**Sample Data:**
```sql
INSERT INTO about_me (full_name, title, bio, profile_image_url)
VALUES (
    'Edon Bytyqi',
    'Software Developer',
    'Experienced software developer specializing in Avaloq, SAP, and .NET development. Passionate about creating efficient and scalable solutions.',
    '/assets/images/profile.jpg'
);
```

---

### 2. `experiences`
Stores work experience entries.

```sql
CREATE TABLE experiences (
    id SERIAL PRIMARY KEY,
    job_title VARCHAR(255) NOT NULL,
    company VARCHAR(255) NOT NULL,
    location VARCHAR(255),
    start_date DATE NOT NULL,
    end_date DATE,
    is_current_job BOOLEAN DEFAULT FALSE,
    responsibilities JSONB, -- Array of strings
    sort_order INTEGER DEFAULT 0,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Index for sorting
CREATE INDEX idx_experiences_sort_order ON experiences (sort_order);
```

**Sample Data:**
```sql
INSERT INTO experiences (job_title, company, location, start_date, end_date, is_current_job, responsibilities, sort_order)
VALUES
(
    'Software Developer - SAP',
    'BSI SA',
    'Lugano, Switzerland',
    '2024-05-01',
    NULL,
    TRUE,
    '["Develop and maintain SAP applications", "Collaborate with cross-functional teams", "Implement new features and optimize existing code", "Participate in code reviews and knowledge sharing"]'::jsonb,
    1
),
(
    'Software Developer - Avaloq',
    'BSI SA',
    'Lugano, Switzerland',
    '2023-07-01',
    '2024-04-30',
    FALSE,
    '["Developed banking solutions using Avaloq platform", "Customized workflows and business processes", "Maintained high code quality standards", "Worked in Agile/Scrum environment"]'::jsonb,
    2
);
```

---

### 3. `education`
Stores education and certifications.

```sql
CREATE TABLE education (
    id SERIAL PRIMARY KEY,
    degree VARCHAR(255) NOT NULL,
    institution VARCHAR(255) NOT NULL,
    location VARCHAR(255),
    start_date DATE NOT NULL,
    end_date DATE,
    grade VARCHAR(100),
    description TEXT,
    sort_order INTEGER DEFAULT 0,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Index for sorting
CREATE INDEX idx_education_sort_order ON education (sort_order);
```

**Sample Data:**
```sql
INSERT INTO education (degree, institution, location, start_date, end_date, grade, description, sort_order)
VALUES (
    'Computer Science Apprenticeship',
    'SPAI - Scuola Professionale Artigianale e Industriale',
    'Lugano, Switzerland',
    '2019-08-01',
    '2023-06-30',
    'Excellent',
    'Comprehensive program covering software development, databases, and system design',
    1
);
```

---

### 4. `skills`
Stores individual skills.

```sql
CREATE TABLE skills (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    category VARCHAR(100) NOT NULL,
    proficiency_level INTEGER CHECK (proficiency_level >= 1 AND proficiency_level <= 5),
    sort_order INTEGER DEFAULT 0,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Index for category and sorting
CREATE INDEX idx_skills_category ON skills (category, sort_order);
```

**Sample Data:**
```sql
INSERT INTO skills (name, category, proficiency_level, sort_order)
VALUES
-- Programming Languages
('C#', 'Programming Languages', 5, 1),
('TypeScript', 'Programming Languages', 4, 2),
('JavaScript', 'Programming Languages', 4, 3),
('SQL', 'Programming Languages', 4, 4),

-- Frameworks & Technologies
('.NET Core', 'Frameworks & Technologies', 5, 1),
('Angular', 'Frameworks & Technologies', 4, 2),
('Entity Framework', 'Frameworks & Technologies', 4, 3),
('ASP.NET MVC', 'Frameworks & Technologies', 5, 4),

-- Tools & Platforms
('Git', 'Tools & Platforms', 4, 1),
('Azure', 'Tools & Platforms', 3, 2),
('Docker', 'Tools & Platforms', 3, 3),
('Visual Studio', 'Tools & Platforms', 5, 4),

-- Domain Expertise
('Avaloq', 'Domain Expertise', 4, 1),
('SAP', 'Domain Expertise', 4, 2),
('Banking Systems', 'Domain Expertise', 4, 3);
```

---

### 5. `contact`
Stores contact information.

```sql
CREATE TABLE contact (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    phone VARCHAR(50),
    location TEXT,
    linkedin_url VARCHAR(500),
    github_url VARCHAR(500),
    website_url VARCHAR(500),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT NOW(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);

-- Only one record should exist
CREATE UNIQUE INDEX idx_contact_singleton ON contact ((1));
```

**Sample Data:**
```sql
INSERT INTO contact (email, phone, location, linkedin_url, github_url, website_url)
VALUES (
    'edonbytyqi@outlook.com',
    '+41 XX XXX XX XX',
    'Lugano, Switzerland',
    'https://linkedin.com/in/edon-bytyqi',
    'https://github.com/EdonBytyqi',
    'https://edonbytyqi.com'
);
```

---

## Row Level Security (RLS) Policies

Enable RLS and create policies for public read access:

```sql
-- Enable RLS on all tables
ALTER TABLE about_me ENABLE ROW LEVEL SECURITY;
ALTER TABLE experiences ENABLE ROW LEVEL SECURITY;
ALTER TABLE education ENABLE ROW LEVEL SECURITY;
ALTER TABLE skills ENABLE ROW LEVEL SECURITY;
ALTER TABLE contact ENABLE ROW LEVEL SECURITY;

-- Allow public read access (for displaying CV)
CREATE POLICY "Public read access" ON about_me FOR SELECT USING (true);
CREATE POLICY "Public read access" ON experiences FOR SELECT USING (true);
CREATE POLICY "Public read access" ON education FOR SELECT USING (true);
CREATE POLICY "Public read access" ON skills FOR SELECT USING (true);
CREATE POLICY "Public read access" ON contact FOR SELECT USING (true);

-- For authenticated users only (you can manage via Supabase dashboard)
-- Optionally add INSERT, UPDATE, DELETE policies for authenticated users
```

---

## Triggers for Updated_At

Automatically update `updated_at` timestamp:

```sql
-- Create function to update timestamp
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
    RETURN NEW;
END;
$$ language 'plpgsql';

-- Create triggers for each table
CREATE TRIGGER update_about_me_updated_at BEFORE UPDATE ON about_me
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_experiences_updated_at BEFORE UPDATE ON experiences
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_education_updated_at BEFORE UPDATE ON education
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_skills_updated_at BEFORE UPDATE ON skills
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();

CREATE TRIGGER update_contact_updated_at BEFORE UPDATE ON contact
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
```

---

## Indexes for Performance

```sql
-- Composite index for skills grouping
CREATE INDEX idx_skills_category_sort ON skills (category, sort_order);

-- Index for experience ordering
CREATE INDEX idx_experiences_sort_current ON experiences (sort_order, is_current_job);

-- Index for education ordering
CREATE INDEX idx_education_dates ON education (start_date DESC, end_date DESC);
```

---

## Setup Instructions for Supabase

1. **Create Project:**
   - Go to [supabase.com](https://supabase.com)
   - Create a new project
   - Note your project URL and anon key

2. **Run SQL Queries:**
   - Go to SQL Editor in Supabase dashboard
   - Copy and paste all the CREATE TABLE statements
   - Run the sample INSERT statements
   - Enable RLS and create policies
   - Add triggers and indexes

3. **Get API Credentials:**
   - Go to Project Settings → API
   - Copy:
     - `Project URL` (e.g., `https://xxxxx.supabase.co`)
     - `anon/public` key

4. **Add to .NET Application:**
   ```json
   // appsettings.json
   {
     "Supabase": {
       "Url": "https://your-project.supabase.co",
       "Key": "your-anon-key"
     }
   }
   ```

---

## Next Steps

1. Install Supabase NuGet package:
   ```bash
   dotnet add package supabase-csharp
   ```

2. Update `CvService.cs` to fetch data from Supabase instead of hardcoded values

3. Add caching layer (optional) to reduce API calls

---

## Security Notes

- ✅ RLS enabled with public read-only access
- ✅ No sensitive data exposed (phone numbers can be partially masked)
- ✅ Anon key is safe to use in frontend (RLS protects write operations)
- ⚠️ For admin operations (INSERT/UPDATE/DELETE), use service role key server-side only
- 🔒 Never commit API keys to Git (use environment variables)

---

## Database Migration Strategy

If you need to update schema later:

1. Create migration SQL files
2. Test in development first
3. Backup production data
4. Run migrations via Supabase dashboard or CLI

---

## Estimated Supabase Costs

**Free Tier Includes:**
- 500 MB database space
- 2 GB bandwidth per month
- 50,000 monthly active users
- Unlimited API requests

For a personal CV website, the free tier is more than sufficient!
