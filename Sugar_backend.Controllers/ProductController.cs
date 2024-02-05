using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;

namespace Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductRepository productRepository)
{
    [HttpGet]
    public JsonResult GetProductByName(string name)
    {
        return new JsonResult(productRepository.GetProductByName(name));
    }

    [HttpGet]
    public JsonResult GetCarbsAmount(string name)
    {
        return new JsonResult(productRepository.GetCarbsAmount(name));
    }

    [HttpGet]
    public JsonResult GetProductContainsValue(string name)
    {
        return new JsonResult(productRepository.GetProductContainsValue(name));
    }

    [HttpPost]
    public void AddProduct(string name, int carbs)
    {
        productRepository.AddProduct(name, carbs);
    }
}