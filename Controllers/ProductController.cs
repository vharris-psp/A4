using Microsoft.AspNetCore.Mvc;
using A4.Models;




namespace A4.Controllers{

public class ProductController : Controller{
    private readonly A4DbContext _context;

    public ProductController(A4DbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        try 
        {
            string productId = GenerateProductId();
            var newProduct = new Product
            {
            Id = productId,
            Price = product.Price,
            AvailableQuantity = product.AvailableQuantity,
            Description = product.Description,
            Name = product.Name
            };  

             _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");

        }catch (Exception ex){
            return View("Create");
        }
    }
    [HttpGet]
    public IActionResult Create()
    {         
        return View();
    }
    [HttpGet]
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }
    private string GenerateProductId()
    {
        return "UPC:" + System.Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
    [HttpGet]
    public IActionResult IncreaseQuantity()
    {
        var products = _context.Products.ToList();

        if (products == null || products.Count == 0)
        {
            ViewData["Message"] = "No products found.";
        }

        ViewData["Products"] = products;
        return View();
    }


    [HttpPost]
    public IActionResult IncreaseQuantity(string productId, int quantityToAdd)
    {
        var product = _context.Products.Find(productId);

        if (product == null)
        {
            return NotFound();
        }

        // Update available quantity
        product.AvailableQuantity += quantityToAdd;

        // Save changes to the database
        _context.SaveChanges();

        // Redirect to product details or any other appropriate page
        return RedirectToAction("Index", "Product");
    }
}
}
