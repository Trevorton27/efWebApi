using efApiTutorial.Data;
using efApiTutorial.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace efApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;

        }

        // GET api/values
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;
            switch (sort)
            {
                case "desc":
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;

            }
            // return _quotesDbContext.Quotes;
            return Ok(quotes);
        }

        [HttpGet("[action]")]

        public IActionResult QuotePaging(int? pageNumber, int? pageSize)
        {
            var quotes = _quotesDbContext.Quotes;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SearchQuote(string type)
        {
          var quote =  _quotesDbContext.Quotes.Where(q => q.Type.StartsWith(type));

            return Ok(quote);

        }


        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
        var quote = _quotesDbContext.Quotes.Find(id);
          if (quote == null)
            {
                return NotFound("No record found");
            }
            else
            {
                return Ok(quote);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            quote.UserId = userId;
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var entity = _quotesDbContext.Quotes.Find(id);
            if (entity==null)
            {
                return NotFound("The record you are attempting to update does not exist.");
            }
            if (userId != entity.UserId)
            {
                return BadRequest("Sorry the user id for this record does not match.");
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();
                return Ok("Record updated successfully.");
            }
      
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          var quote =  _quotesDbContext.Quotes.Find(id);
            if (quote==null)
            {
                return NotFound("The record you are attempting to delete does not exist.");
            }
            else
            {
                _quotesDbContext.Quotes.Remove(quote);
                _quotesDbContext.SaveChanges();
                return Ok("Quote deleted.");
            }
     
        }
    }
}
