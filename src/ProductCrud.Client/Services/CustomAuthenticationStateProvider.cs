using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Net.Http.Json;
using ProductCrud.Client.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace ProductCrud.Client.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, IAccountManagement
    {
        private readonly HttpClient _httpClient;
        private readonly ClaimsPrincipal Unauthenticated = new(new ClaimsIdentity());
        private bool _authenticated = false;
        private string _token = string.Empty;
        private readonly JsonSerializerOptions jsonSerializerOptions =
          new()
          {
              PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          };
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(
            IHttpClientFactory httpClientFactory,
            ILocalStorageService localStorageService)
        {
            _httpClient = httpClientFactory.CreateClient("auth");
            _localStorageService = localStorageService;
        }

        public async Task<FormResult> RegisterAsync(string email, string password)
        {
            string[] defaultDetail = ["An unknown error prevented registration from succeeding."];

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/users/register",
                    new
                    {
                        email,
                        password,
                    });

                if (response.IsSuccessStatusCode)
                {
                    return new FormResult(true);
                }

                var details = await response.Content.ReadAsStringAsync();

                var problemDetails = JsonDocument.Parse(details);
                var errors = new List<string>();
                var errorList = problemDetails.RootElement.GetProperty("errors");
                foreach (var errorEntry in errorList.EnumerateObject())
                {
                    if (errorEntry.Value.ValueKind == JsonValueKind.String)
                    {
                        errors.Add(errorEntry.Value.GetString()!);
                    }
                    else if (errorEntry.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var error in errorEntry.Value.EnumerateArray())
                        {
                            errors.AddRange(
                                errorEntry.Value.EnumerateArray()
                                .Select(e => e.GetString() ?? string.Empty)
                                .Where(e => !string.IsNullOrEmpty(e)));
                        }
                    }
                }

                return new FormResult(IsSuccess: true,
                    ErrorList: problemDetails == null
                        ? defaultDetail
                        : [.. errors]);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<FormResult> LoginAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(
                    "api/users/login", new
                    {
                        email = username,
                        password
                    });

                if (response.IsSuccessStatusCode)
                {
                    var loginResult = await response.Content.ReadFromJsonAsync<LoginDto>();
                    _token = loginResult!.AccessToken!;
                    await _localStorageService.SetItemAsync("accessToken", _token!);
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", _token);
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    return new FormResult(true);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return new FormResult(false, ["Invalid email and/or password."]);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _authenticated = false;

            // default to not authenticated
            var user = Unauthenticated;

            try
            {
                var userResponse = await _httpClient.GetAsync("api/users/manage/info");

                userResponse.EnsureSuccessStatusCode();

                var userJson = await userResponse.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfo>(userJson, jsonSerializerOptions);

                if (userInfo != null)
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userInfo.Email),
                        new(ClaimTypes.Email, userInfo.Email)
                    };

                    claims.AddRange(
                      userInfo.Claims.Where(c => c.Key != ClaimTypes.Name && c.Key != ClaimTypes.Email)
                          .Select(c => new Claim(c.Key, c.Value)));

                    //var rolesResponse = await _httpClient.GetAsync($"api/users/Role/GetuserRole?userEmail={userInfo.Email}");

                    //rolesResponse.EnsureSuccessStatusCode();

                    //var rolesJson = await rolesResponse.Content.ReadAsStringAsync();

                    //var roles = JsonSerializer.Deserialize<string[]>(rolesJson, jsonSerializerOptions);
                    //if (roles != null && roles?.Length > 0)
                    //{
                    //    foreach (var role in roles)
                    //    {
                    //        claims.Add(new(ClaimTypes.Role, role));
                    //    }
                    //}

                    var id = new ClaimsIdentity(claims, nameof(CustomAuthenticationStateProvider));

                    user = new ClaimsPrincipal(id);

                    _authenticated = true;
                }
            }
            catch (Exception)
            {

            }

            return new AuthenticationState(user);
        }

        //public async Task NotifyUserAuthenticationAsync(string token)
        //{
        //    // Create a minimal identity with the token
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "authenticated_user"),
        //        new Claim(ClaimTypes.Authentication, "true"),
        //        new Claim("AccessToken", token)
        //    };

        //    var identity = new ClaimsIdentity(claims, "Bearer");
        //    var user = new ClaimsPrincipal(identity);

        //    var authState = new AuthenticationState(user);
        //    NotifyAuthenticationStateChanged(Task.FromResult(authState));

        //    // Try to get more information after notification to improve UI experience
        //    try
        //    {
        //        await GetAuthenticationStateAsync();
        //    }
        //    catch
        //    {
        //        // Ignore any errors during refresh
        //    }
        //}

        public void NotifyUserLogout()
        {
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(Unauthenticated)));
        }
    }
}