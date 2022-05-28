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
            _context = context;
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

        /*// POST: api/Shows
        [HttpPost]
        public async Task<Location> Create(string name, string owner, string street, int number, string postalNumber, int phoneNumber, string email)
        {

            var location = new Location()
            {
        
                Name = name,
                Owner = owner,
                Street = street,
                Number = number,
                PostalNumber = postalNumber,
                PhoneNumber = phoneNumber,
                Email = email
                
            };
            
            _context.Add(location);
            await _context.SaveChangesAsync();

            return location;
        }*/
        
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
    }
}
