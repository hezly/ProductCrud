using ProductCrud.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace ProductCrud.Domain.Models;

public class Product : Entity<int>
{
    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.01")]
    public decimal Price { get; set; }

    public static Product Create(string name, decimal price, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        return new Product
        {
            Name = name,
            Price = price,
            Description = description
        };
    }
}
