using DotNetCV.Configuration;
using DotNetCV.Services;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

// Configure options with validation
builder.Services.AddOptions<CorsOptions>()
    .Bind(builder.Configuration.GetSection(CorsOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Add response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

// Add CORS
var corsOptions = builder.Configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>()
    ?? throw new InvalidOperationException("CORS configuration is missing");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(corsOptions.AllowedOrigins)
              .WithMethods(corsOptions.AllowedMethods)
              .WithHeaders(corsOptions.AllowedHeaders);

        if (corsOptions.AllowCredentials)
        {
            policy.AllowCredentials();
        }
    });
});

// Add health checks
builder.Services.AddHealthChecks();

// Add services to the container
builder.Services.AddControllersWithViews();

// Built-in OpenAPI document; XML comments are picked up automatically
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, _, _) =>
    {
        document.Info = new()
        {
            Title = "CV API",
            Version = "v1",
            Description = "RESTful API for Edon Bytyqi's CV data",
            Contact = new()
            {
                Name = "Edon Bytyqi",
                Email = "edonbytyqi@outlook.com",
                Url = new Uri("https://edonbytyqi.com")
            }
        };
        return Task.CompletedTask;
    });
});

// The CV content never changes at runtime, so one shared instance is enough
builder.Services.AddSingleton<ICvService, CvService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // OpenAPI document at /openapi/v1.json and Swagger UI at /api/docs, in development only
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "CV API v1");
        options.RoutePrefix = "api/docs";
    });
}

// Security Headers Middleware
app.Use(async (context, next) =>
{
    // Prevent clickjacking
    context.Response.Headers.Append("X-Frame-Options", "DENY");

    // Prevent MIME type sniffing
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");

    // Content Security Policy
    context.Response.Headers.Append("Content-Security-Policy",
        "default-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' data:; font-src 'self' data:; frame-ancestors 'none'");

    // Referrer Policy
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

    // Permissions Policy
    context.Response.Headers.Append("Permissions-Policy", "geolocation=(), microphone=(), camera=()");

    await next();
});

// Enable response compression
app.UseResponseCompression();

app.UseHttpsRedirection();
app.UseRouting();

// Enable CORS
app.UseCors();

app.UseAuthorization();

app.MapStaticAssets();

// Map health check endpoint
app.MapHealthChecks("/health");

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Map API controllers
app.MapControllers();

app.Run();
