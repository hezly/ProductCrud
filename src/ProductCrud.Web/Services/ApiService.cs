using ProductCrud.Web.Models;
using System.Net.Http.Json;

namespace ProductCrud.Web.Services;

public interface IApiService
{
    Task<List<Product>> GetProductsAsync(int PageIndex = 0, int PageSize = 10, string? SearchTerm = null, string? SortBy = null, bool SortDescending = false);
    Task<bool> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
}

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private string _token = string.Empty;

    public ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    // Get all products
    public async Task<List<Product>> GetProductsAsync(int PageIndex = 0, int PageSize = 10, string? SearchTerm = null, string? SortBy = null, bool SortDescending = false)
    {
        try
        {
            // Build the query string with parameters
            var queryParams = new List<string>
        {
            $"PageIndex={PageIndex}",
            $"PageSize={PageSize}"
        };

            if (!string.IsNullOrEmpty(SearchTerm))
                queryParams.Add($"SearchTerm={Uri.EscapeDataString(SearchTerm)}");

            if (!string.IsNullOrEmpty(SortBy))
                queryParams.Add($"SortBy={Uri.EscapeDataString(SortBy)}");

            queryParams.Add($"SortDescending={SortDescending}");

            string queryString = string.Join("&", queryParams);

            // Make the API request with the query parameters
            var response = await _httpClient.GetAsync($"api/Products?{queryString}");

            if (response.IsSuccessStatusCode)
            {
                // Deserialize the paginated response
                var paginatedResult = await response.Content.ReadFromJsonAsync<PaginatedResponse<Product>>();
                return paginatedResult?.Data?.ToList() ?? [];
            }

            return [];
        }
        catch
        {
            return [];
        }
    }

    // Create a product
    public async Task<bool> CreateProductAsync(Product product)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Products", new { product });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // Update a product
    public async Task<bool> UpdateProductAsync(int id, Product product)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{id}", new { product });
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    // Delete a product
    public async Task<bool> DeleteProductAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{id}");
            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}

public class LoginResult
{
    public bool Succeeded { get; set; }
    public string? Error { get; set; }
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}

public class LoginDto
{
    public string? TokenType { get; set; }
    public string? AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string? RefreshToken { get; set; }
}