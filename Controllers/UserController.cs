using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Blogger.Data;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blogger.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext _context)
        {
            this._context = _context;
        }
        // Search Logic
        public async Task<IActionResult> List([FromQuery(Name = "search")] string search_key)
        {
            List<User> users = new List<User>();
            if(string.IsNullOrEmpty(search_key))
            {
                users = await _context.Users.ToListAsync();
                return View(User);
            }
            else
            {
                var Getusers = await _context.Users.Where(x => x.FirstName.ToLower().Contains(search_key.ToLower()) || x.Email.ToLower().Contains(search_key.ToLower()) || x.LastName.ToLower().Contains(search_key.ToLower()) || x.MiddleName.ToLower().Contains(search_key.ToLower())).ToListAsync();
                return View(Getusers);
            }
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id <= 0)
                return BadRequest("Invalid Id: User Not Found");
            else
            {
                var user = await _context.Users.FindAsync(id);
                if(user != null)
                {
                    return View(user);
                }
                return BadRequest("User Not Found");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                User SetUser = new User()
                {
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Password = user.Password,
                    RejisteredAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    Intro = user.Intro,
                    Profile = "/images/user.png"
                };
                await _context.Users.AddAsync(SetUser);
                await _context.SaveChangesAsync();
                TempData["success-messege"] = "User Created Successfully!";
                return RedirectToAction(nameof(List));
            }
            ModelState.AddModelError("", "Check the Required Fields!");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int ID, User user)
        {
            if(ID <= 0)
                return BadRequest("Invalid Id");
            if(ModelState.IsValid)
            {
                var ExistingUser = _context.Users.FirstOrDefault(x => x.Id == ID);
                ExistingUser.FirstName = user.FirstName;
                ExistingUser.Email = user.Email;
                ExistingUser.LastName = user.LastName;
                ExistingUser.MiddleName = user.MiddleName;
                ExistingUser.Phone = user.Phone;
                ExistingUser.Password = user.Password;
                ExistingUser.RejisteredAt = user.RejisteredAt;
                ExistingUser.LastLogin = user.LastLogin;
                ExistingUser.Intro = user.Intro;
                ExistingUser.Profile = user.Profile;
                await _context.SaveChangesAsync(true);
                TempData["success-messege"] = "User Updated Successfully!";
                return RedirectToAction(nameof(List));
            }
            ModelState.AddModelError("", "Check Required Fields");
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return View(user);
        }
        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid ID");
            }
            var model = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(model != null)
            {
                _context.Users.Remove(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            return BadRequest("Usesr Not Found!");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}