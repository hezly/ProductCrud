using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProductCrud.Application.Data;
using ProductCrud.Infrastructure.Identity;
using System.Reflection;

namespace ProductCrud.Infrastructure.Data;

public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
