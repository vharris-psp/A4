using Microsoft.AspNetCore.Mvc;
using A4.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;




namespace A4.Controllers{

public class UserController : Controller{
    private readonly A4DbContext _context;

    public UserController(A4DbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult AddUser(User user)
    {


        string userId = GenerateUserId();
        string accountNumber = GenerateAccountNumber();

        if (!ModelState.IsValid)
        {
            return View(user);
        }
        
        try
        {
            var newUser = new User
            {
                Id = userId,
                AccountNumber = accountNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return RedirectToAction("Index", "Dashboard");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Failed to save user");
            return View(user);
        }
        

    }
    [HttpGet]
    public IActionResult Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var user = _context.Users.Find(id);
        
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string id, [Bind("Id,FirstName,LastName,AccountNumber")] User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(user);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }
    
    
    public IActionResult Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    [HttpPost, ActionName("DeleteConfirmed")]
    public IActionResult DeleteConfirmed(string id)
    {
        var user = _context.Users.Find(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet] 
    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);
    }
    private string GenerateUserId()
    {
        return "USR-" + System.Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
    }
    private string GenerateAccountNumber()
    {
        return "ACCT-" + System.Guid.NewGuid().ToString().Substring(0,8).ToUpper();
    }
    private bool UserExists(string id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}
}
