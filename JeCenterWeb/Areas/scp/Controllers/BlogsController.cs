using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JeCenterWeb.Data;
using JeCenterWeb.Models;
using JeCenterWeb.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Josephabishara.Areas.CP.Controllers
{
    [Area("scp")]
    [AllowAnonymous]

    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        string ControllerName = "Blogs";
        public BlogsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: CP/Blogs
        public async Task<IActionResult> Index()
        {
            ViewData["Create"] = "Create";
            ViewData["Controller"] = ControllerName;
            var applicationDbContext = _context.CBlogs.Where(b => b.Active == true);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: CP/Blogs/Create
        public IActionResult Create()
        {

            ViewData["Controller"] = ControllerName;
            return View();
        }

        // POST: CP/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogsId,BlogImage,BlogTitle,BlogDiscretion,BlogDate,Paragraph")] CPBlogsViewModel blogsViewModel)
        {
            ViewData["Controller"] = ControllerName;

            //if (ModelState.IsValid)
            //{

           

            string imgurlpath = "images/blog/default.jpg";
            if (blogsViewModel.BlogImage != null)
            {
                string folder = "images/blog/";
                folder += Guid.NewGuid().ToString() + "-" + blogsViewModel.BlogImage.FileName;
                imgurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await blogsViewModel.BlogImage.CopyToAsync(new FileStream(serverfolder, FileMode.Create));
            }
            CBlogs blog = new CBlogs
            {
                BlogTitle = blogsViewModel.BlogTitle,
                BlogImage = imgurlpath,
                BlogDiscretion = blogsViewModel.BlogDiscretion,
                BlogDate = blogsViewModel.BlogDate,
                Paragraph = blogsViewModel.Paragraph,

            };

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            blog.CreateId = Convert.ToInt32(userId);
            blog.CreatedDate = DateTime.Now;
            blog.Active = true;
            _context.Add(blog);
            await _context.SaveChangesAsync();
           

            return RedirectToAction(nameof(Index));


            //}
          
        }

        // GET: CP/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Controller"] = ControllerName;
           

            if (id == null || _context.CBlogs == null)
            {
                return NotFound();
            }


            var blogs = await _context.CBlogs.FindAsync(id);
            CPEditBlogsViewModel cPBlogsViewModel = new CPEditBlogsViewModel
            {
                BlogsId = blogs.BlogsId,
                BlogTitle = blogs.BlogTitle,
                BlogDiscretion = blogs.BlogDiscretion,
                BlogDate = blogs.BlogDate,
                Paragraph = blogs.Paragraph,


            };
            if (blogs == null)
            {
                return NotFound();
            }
            return View(cPBlogsViewModel);
        }

        // POST: CP/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogsId,BlogTitle,BlogDiscretion,BlogDate,Paragraph")] CPEditBlogsViewModel blogsViewModel)
        {
            ViewData["Controller"] = ControllerName;
            if (id != blogsViewModel.BlogsId)
            {
                return NotFound();
            }


            CBlogs blogs = await _context.CBlogs.FindAsync(id);

            blogs.BlogTitle = blogsViewModel.BlogTitle;
            blogs.BlogDiscretion = blogsViewModel.BlogDiscretion;
            blogs.BlogDate = blogsViewModel.BlogDate;
            blogs.Paragraph = blogsViewModel.Paragraph;


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            blogs.UpdateId = Convert.ToInt32(userId);
            blogs.UpdatedDate = DateTime.Now;
            blogs.Active = true;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogsExists(blogs.BlogsId))
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
           return View(blogs);
        }

        // GET: CP/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Controller"] = ControllerName;
            if (id == null || _context.CBlogs == null)
            {
                return NotFound();
            }

            var blogs = await _context.CBlogs
                
                .FirstOrDefaultAsync(m => m.BlogsId == id);
            if (blogs == null)
            {
                return NotFound();
            }

            return View(blogs);
        }

        // POST: CP/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["Controller"] = ControllerName;
            if (_context.CBlogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
            }
            CBlogs blogs = await _context.CBlogs.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            blogs.DeleteId = Convert.ToInt32(userId);
            blogs.DeletedDate = DateTime.Now;
            blogs.Active = false;
            if (blogs != null)
            {
                _context.CBlogs.Update(blogs);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogsExists(int id)
        {
            return (_context.CBlogs?.Any(e => e.BlogsId == id)).GetValueOrDefault();
        }
    }
}
