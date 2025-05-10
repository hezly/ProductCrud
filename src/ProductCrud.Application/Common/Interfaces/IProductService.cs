using BuildingBlocks.Pagination;
using ProductCrud.Application.Dtos;
using ProductCrud.Domain.Models;

namespace ProductCrud.Application.Common.Interfaces;

public interface IProductService
{
    Task<PaginatedResult<Product>> GetAllAsync(
        int pageIndex = 0,
        int pageSize = 10,
        string? searchTerm = null,
        string? sortBy = null,
        bool sortDescending = false);
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(Product dto);
    Task<Product?> UpdateAsync(Product dto);
    Task<bool> DeleteAsync(int id);
}