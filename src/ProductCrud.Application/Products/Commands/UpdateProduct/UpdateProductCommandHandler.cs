using BuildingBlocks.CQRS;
using ProductCrud.Application.Common.Interfaces;
using ProductCrud.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler
    (IProductService productService)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productService.GetByIdAsync(request.Product.Id) 
            ?? throw new ProductNotFoundException(request.Product.Id);

        product.Name = request.Product.Name;
        product.Price = request.Product.Price;
        product.Description = request.Product.Description;

        await productService.UpdateAsync(product);

        return new UpdateProductResult(true);
    }
}
