using Microsoft.AspNetCore.Mvc;
using A4.Models;



namespace A4.Controllers
{
    /// <summary>
    /// Controller for managing the dashboard functionality.
    /// </summary>
    public class DashboardController : Controller
    {
        private readonly A4DbContext _context;
        
        /// <summary>
        /// Constructor to inject the database context.
        /// </summary>
        /// <param name="context">The database context.</param>
        public DashboardController(A4DbContext context)
        {
            _context = context;
        }
 
        /// <summary>
        /// Action method for displaying the dashboard index page.
        /// </summary>
        /// <returns>The view for the dashboard index page.</returns>
        public IActionResult Index()
        {
        return View();
        }
    }
}
