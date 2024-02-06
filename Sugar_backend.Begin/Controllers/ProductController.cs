using Microsoft.AspNetCore.Mvc;
using Sugar_backend.Application.Contract.Products;
using Sugar_backend.Application.Models.Users;

namespace begin.Controllers;
public class ProductController(IProductService productService) : Controller
{ 
    public JsonResult GetProductByName(string name)
    {
        return new JsonResult(productService.GetProductByName(name));
    }
    public JsonResult GetCarbsAmountInProduct(string name)
    {
        return new JsonResult(productService.GetCarbsAmountInProduct(name));
    }

    [HttpGet("alikeNames")]
    public JsonResult GetProductContainsValue(string name)
    {
        return new JsonResult(productService.GetProductContainsValue(name));
    }

    [HttpGet("insulinRise")]
    public JsonResult GetInsulinRiseAfterProduct(string name, UserInfo userInfo)
    {
        return new JsonResult(productService.GetInsulinRiseAfterProduct(name, userInfo));
        
    }

    [HttpPost("createProduct/{carbs}")]
    public void PostCreateProduct(string name, int carbs)
    {
        productService.CreateProduct(name, carbs);
    }
}