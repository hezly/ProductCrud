using ProductCrud.Api.Infrastructure;
using ProductCrud.Infrastructure.Identity;

namespace ProductCrud.Api.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapIdentityApi<ApplicationUser>();
    }
}