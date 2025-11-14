using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductCrud.Api.Infrastructure;
using BuildingBlocks.Pagination;
using ProductCrud.Application.Dtos;
using ProductCrud.Application.Products.Queries.GetAllProducts;
using ProductCrud.Application.Products.Commands.CreateProduct;
using ProductCrud.Application.Products.Commands.UpdateProduct;
using ProductCrud.Application.Products.Commands.DeleteProduct;

namespace ProductCrud.Api.Endpoints;

public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetProductsWithPagination)
            .MapPost(CreateProduct)
            .MapPut(UpdateProduct, "{id}")
            .MapDelete(DeleteProduct, "{id}");
    }

    public async Task<Ok<PaginatedResult<ProductDto>>> GetProductsWithPagination(ISender sender, [AsParameters] GetAllProductsQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result.Products);
    }

    public async Task<Created<int>> CreateProduct(ISender sender, CreateProductCommand command)
    {
        var result = await sender.Send(command);
        int id = result.Id;
        return TypedResults.Created($"/{nameof(Products)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateProduct(ISender sender, int id, UpdateProductCommand command)
    {
        if (id != command.Product.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteProduct(ISender sender, int id)
    {
        await sender.Send(new DeleteProductCommand(id));

        return TypedResults.NoContent();
    }
}
