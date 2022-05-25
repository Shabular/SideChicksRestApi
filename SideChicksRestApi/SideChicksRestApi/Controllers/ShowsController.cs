using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        
        public ShowsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/Shows
        [HttpGet]
        public async Task<List<Show>> Get()
        {
            return await _context.Shows.OrderBy(s => s.Id).ToListAsync();
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            return show == null ? NotFound() : Ok(show);
        }

        // POST: api/Shows
        [HttpPost]
        public async Task<Show> Create(string name)
        {

            var show = new Show()
            {
        
                Name = name,
                Date = DateTime.Now,
                Accepted = true,
                
            };
            
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
        public void Delete(int id)
        {
        }
    }
}
