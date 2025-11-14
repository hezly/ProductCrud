using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductCrud.Web;
using ProductCrud.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped(provider =>
    (IAccountManagement)provider.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddTransient<CustomHttpHandler>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiSettings:FrontEndUrl"]!)
});

builder.Services.AddHttpClient("auth", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "https://localhost:7029/");
});

// Configure HTTP client
builder.Services.AddHttpClient("ApiClient", client =>
{
    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    client.BaseAddress = new Uri(builder.Configuration["BaseUrl"] ?? "https://localhost:7029/");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddScoped<IApiService, ApiService>();

await builder.Build().RunAsync();
