using BaseBackend.Controllers;

using Microsoft.AspNetCore.Authentication.Cookies;

using YourNamespace.Controllers; // Make sure to use the correct namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // For MVC support
builder.Services.AddControllers(); // For API support

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register IConfiguration for DI
//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Register TuningDatabaseHandler for DI
builder.Services.AddScoped<TuningDatabaseHandler>();
builder.Services.AddScoped<MyUploadedFileHandler>();
builder.Services.AddScoped<StringReplacementHandler>();
builder.Services.AddScoped<TaskHandler>();
builder.Services.AddScoped<EcuHandler>();



// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/api/login"; // Adjusted for API
        options.LogoutPath = "/api/logout"; // Adjusted for API
        options.Cookie.HttpOnly = true; // Helps mitigate XSS
                                        //        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are only sent over HTTPS
                                        //      options.Cookie.SameSite = SameSiteMode.None; // Required for cross-origin requests
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // Adapts to HTTP or HTTPS
        options.Cookie.SameSite = SameSiteMode.Lax; // Helps with cross-origin issues

    });

// Configure Authorization
builder.Services.AddAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost") // Your frontend URL
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials(); // Allow cookies if using authentication
    });
});

/*builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 44390; // Specify the HTTPS port
});
*/
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

// Enable CORS policy
app.UseCors("AllowFrontend");

//app.UseHttpsRedirection();

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Test Page: Application is Running!");
app.Run();