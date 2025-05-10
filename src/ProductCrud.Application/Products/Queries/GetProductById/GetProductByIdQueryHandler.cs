using BuildingBlocks.CQRS;
using ProductCrud.Application.Common.Interfaces;
using ProductCrud.Application.Dtos;
using ProductCrud.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductService productService) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await productService.GetByIdAsync(request.Id) 
            ?? throw new ProductNotFoundException(request.Id);

        return new GetProductByIdResult(new ProductDto
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Price = result.Price,
            CreatedAt = result.CreatedAt,
        });
    }
}
