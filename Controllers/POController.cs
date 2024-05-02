using Microsoft.AspNetCore.Mvc;
using A4.Models;
using System.Diagnostics.CodeAnalysis;



namespace A4.Controllers{
public class POController : Controller
{  
    private readonly A4DbContext _context;

    public POController(A4DbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {   
        
        var pos = _context.POs.ToList();

        if (pos == null || !pos.Any())
        {
            ViewBag.Message = "No Purchase Orders Available";
            return View (new List<PO>());
        }

        return View(pos);
        
    }
    [HttpGet]
    public IActionResult Create()
    {
        var users = _context.Users.ToList();
        var products = _context.Products.ToList();

        ViewData["Users"] = users;
        ViewData["Products"] = products;
        
        return View();
    }
    [HttpPost]
    public IActionResult AddPO(PO model)
    {

        var product = _context.Products.Find(model.ProductId);
        model.Id = GeneratePOId();
        model.UnitPrice = product?.Price ?? 0;        
        model.Product = product;
        model.Total = model.Quantity * model.UnitPrice;
        model.User = _context.Users.Find(model.UserId);
    
        if (!ModelState.IsValid)
        {
            foreach (var entry in ModelState)
            {
                if (entry.Value.Errors.Count > 0)
                {
                    Console.WriteLine($"Key: {entry.Key}, Error: {entry.Value.Errors.First().ErrorMessage}");
                }
            }

            var users = _context.Users.ToList();
            var products = _context.Products.ToList();

            ViewData["Users"] = users;
            ViewData["Products"] = products;

            return View("Create", model);
        }
        _context.POs.Add(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
        
    }


    private string GeneratePOId()
    {
        DateTime now = DateTime.Now;
        string formattedDateTime = now.ToString("yyyyMMddHHmmss");

        formattedDateTime = formattedDateTime.Replace(" ", "").Replace("-", "").Replace(":", "");
        return "PO:" + System.Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + formattedDateTime;
    }
}
}
