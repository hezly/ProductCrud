using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using ProductCrud.Application.Common.Interfaces;
using ProductCrud.Application.Data;
using ProductCrud.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IProductService productService)
    : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
{
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var result = await productService.GetAllAsync(
            pageIndex: request.PageIndex,
            pageSize: request.PageSize,
            searchTerm: request.SearchTerm,
            sortBy: request.SortBy,
            sortDescending: request.SortDescending);

        return new GetAllProductsResult(
            new PaginatedResult<ProductDto>(
                request.PageIndex, 
                request.PageSize, 
                result.Count,
                [.. result.Data.Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CreatedAt = x.CreatedAt,
                })]
            ));
    }
}
