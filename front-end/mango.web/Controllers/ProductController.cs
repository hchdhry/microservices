using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mango.web.Models;
using mango.web.models.DTO;
using mango.web.Service.IService;
using Newtonsoft.Json;

namespace mango.web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }


    public async Task<IActionResult> Index()
    {
        List<ProductDTO> productDTOs = new();
        ResponseDTO response = await _productService.GetAllProductsAsync();
        if (response != null && response.isSuccess)
        {
            productDTOs = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(response.Result));
        }
        return View(productDTOs);
    }

   

}
