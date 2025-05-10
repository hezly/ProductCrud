using ProductCrud.Client.Models;
using System.Net.Http.Json;

namespace ProductCrud.Client.Services;

public interface IApiService
{
    Task<List<Product>> GetProductsAsync();
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
    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<Product>>("api/Products")
                ?? new List<Product>();
        }
        catch
        {
            return new List<Product>();
        }
    }

    // Create a product
    public async Task<bool> CreateProductAsync(Product product)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Products", product);
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
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{id}", product);
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