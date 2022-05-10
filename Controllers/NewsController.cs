using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsRESTapi.Models;
using NewsRESTapi.Skripts.RssKey;

namespace NewsRESTapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly NewsContext _context;

    public NewsController(NewsContext context)
    {
        _context = context;
    }

    // GET: api/<NewsController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<News>>> GetNews()
    {
        return await _context.News
            .ToListAsync();
    }

    // GET api/<NewsController>/5
    [HttpGet("{string}")]
    public Task<ActionResult<News>> SubstringGetNews(string @string)
    {
        News? news = new();
        var listNews = new List<News>();
        foreach (News deta in _context.News)
        { 
            if (deta.Title != null &&  deta.Title.Contains(@string))
            {
                listNews?.Add(deta);
            }
        }

        return Task.FromResult<ActionResult<News>>(CreatedAtAction(nameof(SubstringGetNews), new {id = 0}, listNews));
    }

    // GET api/<NewsController>/5
    [HttpGet("IdGetNews/{id}")]
    public async Task<ActionResult<News>> IdGetNews(int id)
    {
        News? news = await _context.News.FindAsync(id);

        if (news == null)
        {
            return NotFound();
        }

        return news;
    }

    // POST api/<NewsController>
    [HttpPost]
    public async Task<ActionResult<RssKey>> PostNews([FromBody] RssKey rssKey)
    {
        if (rssKey.Rss == null)
        {
            return  CreatedAtAction(nameof(PostNews), new {id = 0}, _context.News);
        }
        
        foreach (News data in RssPars.Pars(rssKey))
        {
            _context.News.Add(data);
        }

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(PostNews), new {id = 0}, _context.News);
    }

    // PUT api/<NewsController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNews(int id, News news)
    {
        if (id != news.Id)
        {
            return BadRequest();
        }

        _context.Entry(news).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE api/<NewsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        News? todoItem = await _context.News.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }

        _context.News.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}