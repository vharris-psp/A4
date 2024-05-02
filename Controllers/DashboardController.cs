using Microsoft.AspNetCore.Mvc;
using A4.Models;



namespace A4.Controllers
{
    public class DashboardController : Controller
    {
        private readonly A4DbContext _context;
        
        public DashboardController(A4DbContext context)
        {
            _context = context;
        }
 
    public IActionResult Index()
    {
        return View();
    }
    }
}
