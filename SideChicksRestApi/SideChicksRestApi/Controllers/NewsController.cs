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
    public class NewsController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        
        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/Locations
        [HttpGet]
        public async Task<List<News>> Get()
        {
            return await _context.News.OrderBy(n => n.Id).ToListAsync();
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "GetNews")]
        public async Task<IActionResult> Get(int id)
        {
            var news = await _context.News.FindAsync(id);
            return news == null ? NotFound() : Ok(news);
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
        public async Task<News> Create(News news)
        {
            
            _context.Add(news);
            await _context.SaveChangesAsync();

            return news;
        }

        // PUT: api/Shows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, News news)
        {
            if (id != news.Id) return BadRequest();

            _context.Entry(news).State = EntityState.Modified;
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
