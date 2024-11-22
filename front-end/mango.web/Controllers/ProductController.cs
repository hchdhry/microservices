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
    [HttpPost]
    public async Task<ActionResult> Delete(ProductDTO dTO)
    {
        if (dTO == null)
        {
            TempData["error"] = "Product is null";
            return RedirectToAction(nameof(Index));
        }

        if (ModelState.IsValid)
        {
            ResponseDTO response = await _productService.DeleteCoupon(dTO.ProductId);

            // Add null check for response
            if (response != null)
            {
                if (response.isSuccess)
                {
                    TempData["success"] = "Product deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Use null-coalescing operator to provide a default error message
                    TempData["error"] = response.Message ?? "Error deleting product. Please try again.";
                    ModelState.AddModelError("", response.Message ?? "Error deleting product. Please try again.");
                    return View(dTO);
                }
            }
            else
            {
                // Handle case where response is null
                TempData["error"] = "No response received from service";
                ModelState.AddModelError("", "No response received from service");
                return View(dTO);
            }
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
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
