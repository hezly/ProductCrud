using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net.Http.Headers;

namespace ProductCrud.Client.Services;

public class CutomHttpHandler(ILocalStorageService localStorageService)
    : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await localStorageService.GetItemAsync<string>("accessToken");
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
}
