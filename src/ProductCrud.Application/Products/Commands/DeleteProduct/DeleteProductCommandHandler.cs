using BuildingBlocks.CQRS;
using ProductCrud.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductService productService)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var success = await productService.DeleteAsync(request.Id);
        return new DeleteProductResult(success);
    }
}
