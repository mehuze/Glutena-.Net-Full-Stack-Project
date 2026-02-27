using Glutena.Data;           // repository namespace
using Glutena.Data.Abstract;
using Glutena.Models;         // Product model namespace
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ProductController : Controller
{
    private readonly IGenericRepository<Product> _productRepository;

    public ProductController(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
        //Burada yaptığımız şey:

        //Dependency Injection ile repository’yi controller’a bağladık.

        //Artık database’den veri çekebiliriz.
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();
        return View(products);
    }
}
