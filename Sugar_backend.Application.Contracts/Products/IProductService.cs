using Sugar_backend.Application.Models.Products;

namespace Sugar_backendar.Application.Contracts.Products;

public interface IProductService
{
    Product GetProductByName(string name);

    void CreateProduct(string name, int carbs);
}