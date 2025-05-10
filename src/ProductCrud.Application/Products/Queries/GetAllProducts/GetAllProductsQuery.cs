using BuildingBlocks.CQRS;
using BuildingBlocks.Pagination;
using ProductCrud.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery(string? SearchTerm = null, string? SortBy = null, bool SortDescending = false)
    : PaginationRequest, IQuery<GetAllProductsResult>;

public record GetAllProductsResult(PaginatedResult<ProductDto> Products);