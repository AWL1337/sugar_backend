using Sugar_backend.Application.Models.Products;
using Sugar_backend.Application.Models.Users;

namespace Sugar_backendar.Application.Contracts.Products;

public interface IProductService
{
    Product GetProductByName(string name);
    int GetInsulinRiseAfterProduct(string name, UserInfo userInfo);
    int GetCarbsAmountInProduct(string name);

    void CreateProduct(string name, int carbs);
}