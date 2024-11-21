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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(ProductDTO product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(product);
                if (response.isSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", response?.Message ?? "Error creating product. Please try again.");
                    return View(product);
                }
            }
        }
        catch (Exception e)
        {
            
            ModelState.AddModelError("", $"error:{e}");
            return View(product);
        }
        return View(product);



    }

    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var response = await _productService.GetProductByIdAsync(id);
            var product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            return View(product);
            
        }
        catch(Exception e)
        {
            ModelState.AddModelError("",$"error:{e}");
            return View();

        }
    }
    public async Task<ActionResult> Update(int id)
    {
        try
        {
            var response = await _productService.GetProductByIdAsync(id);
            var product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(response.Result));
            return View(product);

        }
        catch (Exception e)
        {
            ModelState.AddModelError("", $"error:{e}");
            return View();

        }
    }




}
