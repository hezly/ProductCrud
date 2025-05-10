using ProductCrud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCrud.Infrastructure.Data;

public class InitialData
{
    public static IEnumerable<Product> GetProducts()
    {
        return
        [
            Product.Create("IPhone X", 500, "A high-end smartphone with a sleek design and advanced features."),
            Product.Create("Samsung 10", 400, "A reliable smartphone with excellent performance and display quality."),
            Product.Create("Huawei Plus", 650, "A premium smartphone offering cutting-edge technology and great camera capabilities."),
            Product.Create("Xiaomi Mi", 450, "An affordable smartphone with impressive features and solid build quality.")
        ];
    }
}
