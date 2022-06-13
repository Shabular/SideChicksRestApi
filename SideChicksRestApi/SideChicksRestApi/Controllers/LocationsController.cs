using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SideChicksRestApi.Data;
using SideChicksRestApi.Models;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace SideChicksRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        
        public LocationsController(ApplicationDbContext context)
        {
            Console.WriteLine("test");
            _context = context;
            var locationList = context.Locations.ToList();
            if (locationList.Count == 0)
            {
                SeedLocations(_context);
            }
        }
        
        // GET: api/Locations
        [HttpGet]
        public async Task<List<Location>> Get()
        {
            return await _context.Locations.OrderBy(l => l.Id).ToListAsync();
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "GetLocation")]
        public async Task<IActionResult> Get(int id)
        {
            var show = await _context.Locations.FindAsync(id);
            return show == null ? NotFound() : Ok(show);
        }

        // POST: api/Shows
        [HttpPost]
        public async Task<Location> Create(Location location)
        {
            
            _context.Add(location);
            await _context.SaveChangesAsync();

            return location;
        }

        // PUT: api/Shows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Location location)
        {
            if (id != location.Id) return BadRequest();

            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        public static bool SeedLocations(ApplicationDbContext context)
        {

            try
            {
                
                    
                    var userList = context.Users.ToList();
                    var adminUser = userList.Find(u => u.UserName is "admin");
                    var location = new Location
                    {
                        UserId  = adminUser.Id,
                        Name    = "Test Locatie",
                        Owner   = $"{adminUser.FirstName} {adminUser.LastName}",
                        City    = "Breda",
                        Street  = "havenmarkt",
                        Number  = 7,
                        PostalNumber = "1234KK",
                        PhoneNumber = 0654737288,
                        Email   = adminUser.Email
                    };
                    context.Add(location);
                    context.SaveChangesAsync();
                
            }
            catch
            {
                throw new InvalidOperationException();
            }


            return true;
        }
        
    }
    
    
}
