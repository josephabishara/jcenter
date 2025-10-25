using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models.ViewModels;
using JeCenterWeb.Models;
 

namespace JeCenterWeb.Controllers;

[AllowAnonymous]
public class BlogsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BlogsController(ApplicationDbContext context)
    {
        _context = context;
    }
    // All
    public async Task<IActionResult> Index()
    {
        var blogs = await _context.CBlogs
            .Where(b => b.Active == true)
         .ToListAsync();

        return View( blogs);
    }

    // Article
    public async Task<IActionResult> Article(int id)
    {
        var article = await _context.CBlogs.FindAsync(id);
        return View(article);
    }

   
}
