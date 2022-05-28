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
            
            _context.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
}