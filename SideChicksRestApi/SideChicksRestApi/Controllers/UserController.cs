using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SideChicksRestApi.Data;
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
}