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
    public class ShowsController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        private static readonly DateTime later = DateTime.UtcNow.AddDays(40);
        
        public ShowsController(ApplicationDbContext context)
        {
            _context = context;
            var showsList = context.Shows.ToList();
            if (showsList.Count == 0)
            {
                SeedShows(_context);
            }
        }
        
        // GET: api/Shows
        [HttpGet]
        public async Task<List<Show>> Get()
        {
            return await _context.Shows.OrderBy(s => s.Id).ToListAsync();
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "GetShow")]
        public async Task<IActionResult> Get(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            return show == null ? NotFound() : Ok(show);
        }

        // POST: api/Shows
        [HttpPost]
        public async Task<Show> Create(Show show)
        {
            
            _context.Add(show);
            await _context.SaveChangesAsync();

            return show;
        }

        // PUT: api/Shows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Show show)
        {
            if (id != show.Id) return BadRequest();

            _context.Entry(show).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();

        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
            return NoContent();
        }
      
        public static bool SeedShows(ApplicationDbContext context)
        {

            try
            {
                var userList = context.Users.ToList();
                if (userList.Count == 0)
                {
                    UserController.SeedUsers(context);
                    userList = context.Users.ToList();
                }
                
                var locationList = context.Locations.ToList();
                if (locationList.Count == 0)
                {
                    LocationsController.SeedLocations(context);
                    locationList = context.Locations.ToList();
                }
                var testLocation = locationList.Find(l => l.Name is "Test Locatie");
                    
               
                    
                var adminUser = userList.Find(u => u.UserName is "admin");
                var show = new Show()
                {
                    UserId = adminUser.Id,
                    Name = "testShowAccepted",
                    Image = "data:image/jpeg;base64,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",
                    Details = "TestingPurposes only",
                    Fee = 5.0,
                    LocationId = testLocation.Id,
                    Date = later,
                    Accepted = true
                };
                context.Add(show);
                
                show = new Show()
                {
                    UserId = adminUser.Id,
                    Name = "testShowNotAccepted",
                    Image = "data:image/jpeg;base64,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",
                    Details = "TestingPurposes only",
                    Fee = 5.0,
                    LocationId = testLocation.Id,
                    Date = later,
                    Accepted = false
                };
                
                context.Add(show);
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
