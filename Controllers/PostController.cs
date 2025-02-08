using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNetCoreGeneratedDocument;
using Blogger.Data;
using Blogger.Enums;
using Blogger.Helpers.CustomAttributes;
using Blogger.Models;
using Blogger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        // private List<Post> _posts = PostService.GetAllPosts();

        //Search logic
        public async Task<IActionResult> List([FromQuery(Name = "search")] string search_key, Status? status)
        {
            List<Post> posts = new List<Post>();
            if (string.IsNullOrEmpty(search_key) && status == null)
            {
                posts = await _context.Posts.ToListAsync();
                return View(posts);
            }
            else if (status == null && !string.IsNullOrEmpty(search_key))
            {
                var post = await _context.Posts.Where(x => x.Content.ToLower().Contains(search_key.ToLower())).ToListAsync();
                return View(post);
            }
            else if (status != null && string.IsNullOrEmpty(search_key))
            {
                var post = await _context.Posts.Where(x => x.Status == status).ToListAsync();
                return View(post);
            }
            else
            {
                var post = await _context.Posts.Where(x => x.Content.ToLower().Contains(search_key.ToLower()) && x.Status == status).ToListAsync();
                return View(post);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await BindSelectListCategories();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Post post)
        {
            if (ModelState.IsValid)
            {
                Post Post = new Post()
                {
                    Content = post.Content,
                    Slug = post.Title.GetBlogUrl(),
                    CategoryId = post.Category.Id,
                    Category = post.Category,
                    Status = post.Status,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    PublishedAt = DateTime.Now,
                    Title = post.Title
                };

                await _context.Posts.AddAsync(Post);
                await _context.SaveChangesAsync();
                TempData["success-messege"] = "Post created successfully!";
                return RedirectToAction("List");
            }
            ModelState.AddModelError(" ", "Check Required Fields!");
            await BindSelectListCategories();
            return View(post);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
                return NotFound("Post Not Found!");
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                var ExistingPosts = _context.Posts.FirstOrDefault(x => x.Id == id);
                ExistingPosts.CategoryId = post.CategoryId;
                ExistingPosts.Content = post.Content;
                ExistingPosts.CreatedAt = post.CreatedAt;
                ExistingPosts.PublishedAt = post.PublishedAt;
                ExistingPosts.UpdatedAt = post.UpdatedAt;
                ExistingPosts.Slug = post.Title.GetBlogUrl();
                ExistingPosts.Media = post.Media;
                ExistingPosts.Status = post.Status;
                ExistingPosts.UserId = post.UserId;
                ExistingPosts.Title = post.Title;
                await _context.SaveChangesAsync(true);
                TempData["success-messege"] = "Post Updated Successfully!";
                return RedirectToAction(nameof(List));
            }
            ModelState.AddModelError(" ", "Check Required Fields!");
            return View(post);
        }
        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var model = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (model != null)
                return View(model);
            return NotFound("Post Not Found!");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid Id");
            var model = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
                return NotFound("Post Not Found!");

            _context.Posts.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
        private async Task BindSelectListCategories()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}