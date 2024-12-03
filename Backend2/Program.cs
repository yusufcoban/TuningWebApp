using BaseBackend.Controllers;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;

using TuningWebApp.Controllers;
using TuningWebApp.Handler;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();               // Adds console logging, outputs to stdout
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging")); // Optional: Loads logging settings from configuration

// Add services to the container.
builder.Services.AddControllersWithViews(); // For MVC support
builder.Services.AddControllers(); // For API support

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("CookieAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Name = "Cookie",
        In = ParameterLocation.Header,
        Description = "Cookie-based authentication"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CookieAuth"
                }
            },
            new string[] { }
        }
    });
});

// Register services for dependency injection
builder.Services.AddScoped<TuningDatabaseHandler>();
builder.Services.AddScoped<MyUploadedFileHandler>();
builder.Services.AddScoped<StringReplacementHandler>();
builder.Services.AddScoped<TaskHandler>();
builder.Services.AddScoped<UserHandler>();
builder.Services.AddScoped<EcuHandler>(); 

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/login";
        options.LogoutPath = "/api/logout";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None; // Required for cross-origin requests
    });

// Configure Authorization
builder.Services.AddAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("https://ia.sgtuners.es", "http://localhost:5173") // Include both origins here
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Allow cookies if using authentication
    });
});

// Build and configure the HTTP request pipeline
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Enable HTTPS redirection and CORS
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "Test Page: Application is Running!");

app.Run();
