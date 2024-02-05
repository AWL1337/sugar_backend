using Sugar_backend.Application.Models.Products;

namespace Sugar_backend.Application.Abstraction.Repositories;

public interface IProductRepository
{
    Product? GetProductByName(string name);
    public IEnumerable<Product> GetProductContainsValue(string value);


    int GetCarbsAmount(string name);

    void AddProduct(string name, int carbs);
}