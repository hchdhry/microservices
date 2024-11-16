using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mango.web.Models;
using mango.web.models.DTO;

namespace mango.web.Controllers;

public class ProductController : Controller
{
    

    public ProductController()
    {
        
    }

    public IActionResult Index()
    {
        
        return View();
    }

   

}
