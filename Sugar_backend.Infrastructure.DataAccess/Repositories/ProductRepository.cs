using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Products;

namespace Sugar_backend.Infrastructure.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    public Product? GetProductByName(string name)
    {
        throw new NotImplementedException();
    }

    public void AddProduct(string name, int carbs)
    {
        throw new NotImplementedException();
    }
}