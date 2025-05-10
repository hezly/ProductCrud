using System.ComponentModel.DataAnnotations;

namespace ProductCrud.Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.01")]
    public decimal Price { get; set; }

    public DateTime? CreatedAt { get; set; }
}
