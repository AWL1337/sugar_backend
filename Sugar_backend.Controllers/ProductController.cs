using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Abstractions.Repositories;

namespace Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductRepository productRepository)
{
    private IProductRepository _productRepository { get; } = productRepository;

    [HttpGet]
    public JsonResult GetProductByName(string name)
    {
        return new JsonResult(_productRepository.GetProductByName(name));
    }

    [HttpGet]
    public JsonResult GetCarbsAmount(string name)
    {
        return new JsonResult(_productRepository.GetCarbsAmount(name));
    }

    [HttpPost]
    public void AddProduct(string name, int carbs)
    {
        _productRepository.AddProduct(name, carbs);
    }
}