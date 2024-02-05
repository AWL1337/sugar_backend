using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Abstraction.Repositories;
using Sugar_backend.Application.Contract.Products;
using Sugar_backend.Application.Models.Users;

namespace Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IProductService productService)
{
    [HttpGet]
    public JsonResult GetProductByName(string name)
    {
        return new JsonResult(productService.GetProductByName(name));
    }

    [HttpGet]
    public JsonResult GetCarbsAmountInProduct(string name)
    {
        return new JsonResult(productService.GetCarbsAmountInProduct(name));
    }

    [HttpGet]
    public JsonResult GetProductContainsValue(string name)
    {
        return new JsonResult(productService.GetProductContainsValue(name));
    }

    [HttpGet]
    public JsonResult GetInsulinRiseAfterProduct(string name, UserInfo userInfo)
    {
        return new JsonResult(productService.GetInsulinRiseAfterProduct(name, userInfo));
        
    }

    [HttpPost]
    public void PostCreateProduct(string name, int carbs)
    {
        productService.CreateProduct(name, carbs);
    }
}