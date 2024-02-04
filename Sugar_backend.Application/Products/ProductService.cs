using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Products;
using Sugar_backend.Application.Models.Products;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backend.Application.Products;

public class ProductService(IProductRepository repository) : IProductService
{
    public Product? GetProductByName(string name)
    {
        return repository.GetProductByName(name);
    }

    public int GetInsulinRiseAfterProduct(string name, UserInfo userInfo)
    {
        var carbs = GetCarbsAmountInProduct(name);

        return (carbs / userInfo.grainUnit) * userInfo.carbohydrateRatio;

    }

    public int GetCarbsAmountInProduct(string name)
    {
        return repository.GetCarbsAmount(name);
    }

    public void CreateProduct(string name, int carbs)
    {
        repository.AddProduct(name, carbs);
    }
}