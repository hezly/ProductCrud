using ProductCrud.Client.Models;

namespace ProductCrud.Client.Services;

public interface IAccountManagement
{
    Task<FormResult> RegisterAsync(string email, string password);
    Task<FormResult> LoginAsync(string username, string password);
}
