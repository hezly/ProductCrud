using Blazored.LocalStorage;
using Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using ProductCrud.Client.Components;
using ProductCrud.Client.Services;
using System.Text.Encodings.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add services to the container.
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState(); // Add this line

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = "Bearer";
//    options.DefaultChallengeScheme = "Bearer";
//})
//.AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, NoOpAuthenticationHandler>("Bearer", options => { });

// Ensure ProtectedLocalStorage is registered
builder.Services.AddBlazoredLocalStorage();

// Make sure the class name matches your actual implementation
// If your class is named CustomerAuthStateProvider, use that instead
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(provider =>
    (IAccountManagement)provider.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddTransient<CutomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiSettings:FrontEndUrl"]!)
});

builder.Services.AddHttpClient("auth", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});

// Configure HTTP client
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddScoped<IApiService, ApiService>();

var app = builder.Build();

await builder.Build().RunAsync();