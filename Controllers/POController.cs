using Microsoft.AspNetCore.Mvc;
using A4.Models;
using System.Linq;
using System.Collections.Generic;

namespace A4.Controllers
{
    /// <summary>
    /// Controller for managing purchase orders.
    /// </summary>
    public class POController : Controller
    {
        private readonly A4DbContext _context;

        /// <summary>
        /// Constructor to inject the database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public POController(A4DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Action method for displaying the list of purchase orders.
        /// </summary>
        /// <returns>The view for the list of purchase orders.</returns>
        [HttpGet]
        public IActionResult Index()
        {   
            var pos = _context.POs.ToList();

            if (pos == null || !pos.Any())
            {
                ViewBag.Message = "No Purchase Orders Available";
                return View(new List<PO>());
            }

            return View(pos);
        }

        /// <summary>
        /// Action method for displaying the form to create a new purchase order.
        /// </summary>
        /// <returns>The view for creating a new purchase order.</returns>
        [HttpGet]
        public IActionResult Create()
        {
            var users = _context.Users.ToList();
            var products = _context.Products.ToList();

            ViewData["Users"] = users;
            ViewData["Products"] = products;
            
            return View();
        }

        /// <summary>
        /// Action method for adding a new purchase order.
        /// </summary>
        /// <param name="model">The purchase order model to add.</param>
        /// <returns>The view for creating a new purchase order if the model is invalid, 
        /// otherwise redirects to the list of purchase orders.</returns>
        [HttpPost]
        public IActionResult AddPO(PO model)
        {
            var product = _context.Products.Find(model.ProductId);

            if (product == null)
            {
                ModelState.AddModelError("ProductId", "Invalid product selected.");
            }
            else if (model.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than zero.");
            }
            else if (model.Quantity > product.AvailableQuantity)
            {
                ModelState.AddModelError("Quantity", "Not enough quantity available.");
            }
            else
            {
                // Sufficient quantity available, subtract from available quantity
                product.AvailableQuantity -= model.Quantity;
                model.Id = GeneratePOId();
                model.UnitPrice = product.Price;
                model.Total = model.Quantity * model.UnitPrice;
                model.Product = product;
                model.User = _context.Users.Find(model.UserId);

                if (ModelState.IsValid)
                {
                    _context.POs.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            var users = _context.Users.ToList();
            var products = _context.Products.ToList();
            ViewData["Users"] = users;
            ViewData["Products"] = products;

            return View("Create", model);
        }

        /// <summary>
        /// Generates a unique purchase order ID based on the current date and time.
        /// </summary>
        /// <returns>A unique purchase order ID.</returns>
        private string GeneratePOId()
        {
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyyMMddHHmmss");

            formattedDateTime = formattedDateTime.Replace(" ", "").Replace("-", "").Replace(":", "");
            return "PO:" + System.Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + formattedDateTime;
        }
    }
}
