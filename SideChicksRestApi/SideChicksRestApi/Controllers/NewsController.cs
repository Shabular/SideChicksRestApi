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
            var newsList = context.News.ToList();
            if (newsList.Count == 0)
            {
                SeedNews(_context);
            }
        }
        
        // GET: api/Locations
        [HttpGet]
        public async Task<List<News>> Get()
        {
            var newsList = await _context.News.OrderBy(n => n.Id).ToListAsync();

            foreach (var newsItem in newsList)
            {
                //here we get the base 64
                ImageController imageController = new();
                newsItem.Image = await imageController.ToBase64Photo(newsItem.Image);
            }

            return newsList;
        }

        // GET: api/Shows/5
        [HttpGet("{id}", Name = "GetNews")]
        public async Task<IActionResult> Get(int id)
        {
            var news = await _context.News.FindAsync(id);
            //here we get the base 64
            ImageController imageController = new();
            news.Image = await imageController.ToBase64Photo(news.Image);
            return news == null ? NotFound() : Ok(news);
        }

     
        // POST: api/Shows
        [HttpPost]
        public async Task<News> Create(News news)
        {
            string imageName = $"{news.Title}";
            
            ImageController imageController = new();
            var filepath = await imageController.FromBase64Photo(news.Image, imageName, "NewsPhoto");
            
            news.Image = filepath;
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
        
        public static bool SeedNews(ApplicationDbContext context)
        {

            try
            {
                var userList = context.Users.ToList();
                if (userList.Count == 0)
                {
                    UserController.SeedUsers(context);
                    userList = context.Users.ToList();
                }
                var adminUser = userList.Find(u => u.UserName is "admin");

                var newsList = new List<News>();
                var newsItem = new News
                {
                    Userid = adminUser.Id,
                    Title = "One day, maybe",
                    Details = "No news jet, this will be posted one day",
                    Image = "..\\Images\\sidechicks.png"
                };
                newsList.Add(newsItem);
                
                newsItem = new News
                {
                    Userid = adminUser.Id,
                    Title = "There will be news",
                    Details = "we might be posting soon",
                    Image = "..\\Images\\sidechicks.png"
                };
                newsList.Add(newsItem);
                context.News.AddRange(newsList);
                context.SaveChanges();
                
            }
            catch
            {
                throw new InvalidOperationException();
            }


            return true;
        }
    }
}
