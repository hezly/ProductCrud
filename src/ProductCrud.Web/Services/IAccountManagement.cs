using ProductCrud.Web.Models;

namespace ProductCrud.Web.Services;

public interface IAccountManagement
{
    Task<FormResult> RegisterAsync(string email, string password);
    Task<FormResult> LoginAsync(string username, string password);
}
