using BuildingBlocks.Pagination;
using ProductCrud.Application.Common.Interfaces;
using ProductCrud.Application.Dtos;
using ProductCrud.Infrastructure.Data;
using System.Globalization;
using System.Linq.Expressions;

namespace ProductCrud.Infrastructure.Services;

public class ProductService(ApplicationDbContext context) 
    : IProductService
{
    public async Task<PaginatedResult<Product>> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        string? searchTerm = null,
        string? sortBy = null,
        bool sortDescending = false)
    {
        // Start with base query
        IQueryable<Product> query = context.Products;

        // Apply search filtering if search term provided
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            searchTerm = searchTerm.Trim().ToLower();
            query = query.Where(p =>
                p.Name.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            // Determine property to sort by
            Expression<Func<Product, object>> sortExpression = sortBy.ToLower() switch
            {
                "name" => p => p.Name,
                "price" => p => p.Price,
                "description" => p => p.Description,
                "createdat" => p => p.CreatedAt ?? DateTime.MinValue,
                _ => p => p.Id // Default sort by Id
            };

            // Apply sort direction
            query = sortDescending
                ? query.OrderByDescending(sortExpression)
                : query.OrderBy(sortExpression);
        }
        else
        {
            // Default sort by Id if no sort specified
            query = sortDescending
                ? query.OrderByDescending(p => p.Id)
                : query.OrderBy(p => p.Id);
        }

        // Get total count
        var totalCount = await query.CountAsync();

        // Apply pagination
        var paginatedItems = await query
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Return paginated result
        return new PaginatedResult<Product>(
            pageIndex,
            pageSize,
            totalCount,
            paginatedItems);
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        return product == null ? null : new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
    }

    public async Task<Product> CreateAsync(Product dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description
        };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        dto.Id = product.Id;
        return dto;
    }

    public async Task<Product?> UpdateAsync(Product dto)
    {
        var product = await context.Products.FindAsync(dto.Id);
        if (product == null) return null;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description = dto.Description;

        await context.SaveChangesAsync();
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product == null) return false;

        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return true;
    }
}