using BuildingBlocks.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id)
    : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool Success);
