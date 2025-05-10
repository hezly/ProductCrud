using Microsoft.EntityFrameworkCore;
using ProductCrud.Domain.Models;

namespace ProductCrud.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
