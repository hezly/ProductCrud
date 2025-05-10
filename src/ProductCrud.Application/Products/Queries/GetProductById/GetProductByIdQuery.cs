using BuildingBlocks.CQRS;
using ProductCrud.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(int Id)
    : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(ProductDto Product);