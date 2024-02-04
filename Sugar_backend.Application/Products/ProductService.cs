using Sugar_backend.Application.Abstractions.Repositories;
using Sugar_backend.Application.Models.Products;
using Sugar_backend.Application.Models.Users;
using Sugar_backendar.Application.Contracts.Products;

namespace Sugar_backend.Application.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    
    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public Product GetProductByName(string name)
    {
        return _repository.GetProductByName(name);
    }

    public int GetInsulinRiseAfterProduct(string name, UserInfo userInfo)
    {
        var carbs = GetCarbsAmountInProduct(name);

        return (carbs / userInfo.grainUnit) * userInfo.carbohydrateRatio;

    }

    public int GetCarbsAmountInProduct(string name)
    {
        return _repository.GetCarbsAmount(name);
    }

    public void CreateProduct(string name, int carbs)
    {
        _repository.AddProduct(name, carbs);
    }
}