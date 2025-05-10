using BuildingBlocks.CQRS;
using ProductCrud.Application.Common.Interfaces;
using ProductCrud.Application.Data;
using ProductCrud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductService productService)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(
            request.Product.Name,
            request.Product.Price,
            request.Product.Description);

        var createdProduct = await productService.CreateAsync(newProduct);

        return new CreateProductResult(createdProduct.Id);
    }
}
