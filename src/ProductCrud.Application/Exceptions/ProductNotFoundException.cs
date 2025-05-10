using BuildingBlocks.Exceptions;

namespace ProductCrud.Application.Exceptions;

class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(int id) : base($"Product", id)
    {
    }
}
