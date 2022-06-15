using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SideChicksRestApi.Data;
using SideChicksRestApi.Helpers;
using SideChicksRestApi.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace SideChicksRestApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        
        public UserController(ApplicationDbContext context)
        {
            _context = context;
            var userList = context.Locations.ToList();
            if (userList.Count == 0)
            {
                SeedUsers(_context);
            }
        }
        
        // GET: api/Locations
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _context.Users.OrderBy(l => l.Id).ToListAsync();
        }
        
        [HttpPost]
        public async Task<User> Create(User user)
        {
            // check if username is taken and if email is valid
            var userNameCheck = _context.Users.ToList()
                .Find(u => u.UserName == user.UserName);

            if (userNameCheck != null)
            {
                user.UserName = "Already Taken";
                return user;
            }

            // check if username is taken and if email is valid
            var userMailCheck = _context.Users.ToList()
                .Find(u => u.Email == user.Email);

            if (userMailCheck != null)
            {
                user.Email = "Already Taken";
                return user;
            }

            user.Email = EmailHelper.userMailCheck(user.Email);
            if (user.Email == "Not eligable")
                return user;
            
            var userToAdd = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Instrument = user.Instrument,
                Password = user.Password,
                Email = user.Email

            };
            
            _context.Add(userToAdd);
            await _context.SaveChangesAsync();

            return userToAdd;
        }
        
        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            
            var user = await _context.Users.FindAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
            
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Change(string id, User user)
        {
            
            if (id != user.Id) return BadRequest();

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        public static bool SeedUsers(ApplicationDbContext context)
        {
            var userList = new List<User>();

            try
            {
                userList = context.Users.ToList();
                var admin = userList.Find(u => u.UserName == "admin");
                var user = new User
                {
                    UserName    = "admin",
                    FirstName   = "AdminAccount",
                    Password    = "Welkom01!",
                    Email       = "Admin@sidechicks.nl",
                    IsAdmin     = true
                    
                };
                context.Add(user);
                context.SaveChangesAsync();
            }
            catch
            {
                Console.WriteLine("Seeding failed");



            }

            
            return true;
        }
        
}