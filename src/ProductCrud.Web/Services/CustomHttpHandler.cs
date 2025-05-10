using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;

namespace ProductCrud.Web.Services;

public class CustomHttpHandler(ILocalStorageService localStorageService)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await localStorageService.GetItemAsStringAsync("accessToken", cancellationToken);
        //request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
