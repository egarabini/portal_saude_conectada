using PortalSaudeConectada.Web;
using PortalSaudeConectada.Web.Components;
using PortalSaudeConectada.Web.Services.Auth;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

builder.Services.AddMudServices();

// Add authentication services
builder.Services.AddScoped<ITokenStorageService, TokenStorageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthorizationMessageHandler>();
builder.Services.AddCascadingAuthenticationState();

// Add authentication (required for Blazor Server even with custom AuthenticationStateProvider)
builder.Services.AddAuthentication();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireClaim("tipo", "Administrador"));

    options.AddPolicy("GestorOrAdmin", policy =>
        policy.RequireClaim("tipo", "Administrador", "Gestor"));

    options.AddPolicy("Authenticated", policy =>
        policy.RequireAuthenticatedUser());
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Get API base URL from configuration
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? "https+http://apiservice";

builder.Services.AddHttpClient<WeatherApiClient>(client =>
    {
        client.BaseAddress = new(apiBaseUrl);
    })
    .AddHttpMessageHandler<AuthorizationMessageHandler>();

// Add HttpClient for AuthService (sem o handler para evitar loop)
builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
    {
        client.BaseAddress = new(apiBaseUrl);
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseOutputCache();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();
